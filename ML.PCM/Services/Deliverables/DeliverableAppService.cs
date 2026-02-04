using Microsoft.EntityFrameworkCore;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Deliverables;
using ML.PCM.Managers.Deliverables;
using ML.PCM.Services.Dtos.Deliverables;
using System.Linq.Dynamic.Core;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace ML.PCM.Services.Deliverables;


public class DeliverableAppService : ApplicationService, IDeliverableAppService
{
    private readonly IRepository<Deliverable, Guid> _repository;
    private readonly DeliverableManager _manager;

    public DeliverableAppService(
        IRepository<Deliverable, Guid> repository,
        IRepository<DecisionDocument, Guid> decisionRepository,
        DeliverableManager manager)
    {
        _repository = repository;
        _manager = manager;
    }

    // 获取单个详情
    public async Task<DeliverableDto> GetAsync(Guid id)
    {
        var queryable = await _repository.GetQueryableAsync();
        var entity = await AsyncExecuter.FirstOrDefaultAsync(
            queryable
                .Include(x => x.Contract)
                .Where(x => x.Id == id)
        );

        // 如果找不到抛出异常
        if (entity == null) throw new EntityNotFoundException(typeof(Deliverable), id);

        return ObjectMapper.Map<Deliverable, DeliverableDto>(entity);
    }

    // 获取分页列表
    public async Task<PagedResultDto<DeliverableDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        // 1. 获取 Queryable
        var queryable = await _repository.GetQueryableAsync();

        // 2. 应用排序 (如果没有指定排序，默认按 Name 排序，你可以改为 CreationTime DESC)
        var query = queryable
            .Include(x => x.Contract)
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "CreationTime DESC" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        // 3. 执行查询
        var entitys = await AsyncExecuter.ToListAsync(query);

        // 4. 获取总数 (用于分页)
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        // 5. 映射并返回
        return new PagedResultDto<DeliverableDto>(
            totalCount,
            ObjectMapper.Map<List<Deliverable>, List<DeliverableDto>>(entitys)
        );
    }

    // 创建
    //[Authorize(PCMPermissions.Projects.Create)]
    public async Task<DeliverableDto> CreateAsync(CreateUpdateDeliverableDto input)
    {
        // 校验：虽然前端选了对象，但我们要防一手空指针
        if (input.Contract == null)
        {
            throw new UserFriendlyException("必须关联一个项目");
        }

        // 1. 使用 Manager 创建带有自动编号的实体
        var entity = await _manager.CreateAsync(input.Contract.Id, input.Name);
        // 2. 将 DTO 中其他的字段 (TotalInvestment, Manager等) 映射到实体上
        // 注意：我们需要配置 Mapper 不覆盖 ProjectCode 和 Id
        ObjectMapper.Map(input, entity);

        // 3. 保存到数据库
        await _repository.InsertAsync(entity);

        // 4. 手动构建返回结果
        // 先把 entity 转成 DTO (因为第一步改了可空，这里不会报错了，但 dto.Project 是 null)
        var dto = ObjectMapper.Map<Deliverable, DeliverableDto>(entity);

        dto.Contract = input.Contract;
        dto.ContractId = input.Contract.Id;

        return dto;
    }

    // 更新
    //[Authorize(PCMPermissions.Projects.Edit)]
    public async Task<DeliverableDto> UpdateAsync(Guid id, CreateUpdateDeliverableDto input)
    {
        // 1. 获取实体 (这里保留 Include 也没事，或者去掉 Include 也可以
        // 必须使用 GetQueryableAsync + Include 来加载关联项目，
        // 否则最后一步映射 DTO 时，entity.Project 为空会导致崩溃。
        var queryable = await _repository.GetQueryableAsync();
        var entity = await AsyncExecuter.FirstOrDefaultAsync(
            queryable.Include(x => x.Contract)
                     .Where(x => x.Id == id)
        );

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Deliverable), id);
        }

        // 映射表单修改 (Mapperly 会自动忽略 Id 和 DocumentCode)
        // 此时 input.Project 可能有值，但我们在 Mapper 里配置了忽略 input.Project 对象映射，
        // 只会映射 ProjectId，这是正确的。
        // 2. 更新字段 (ProjectId 会被更新)
        ObjectMapper.Map(input, entity);


        // 3. 保存
        await _repository.UpdateAsync(entity);

        // 4. 映射返回结果
        var dto = ObjectMapper.Map<Deliverable, DeliverableDto>(entity);

        // ✅5. 手动修正返回的 Project 信息
        // 如果 input 里传了最新的 Project 对象，直接赋给 dto
        // 这样能确保前端拿到的返回值里，项目名称是更新后的（比如 "项目 B"）
        if (input.Contract != null)
        {
            dto.Contract = input.Contract;
            dto.ContractId = input.Contract.Id;
        }

        return dto;
    }

    // 删除
    //[Authorize(PCMPermissions.Projects.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}

