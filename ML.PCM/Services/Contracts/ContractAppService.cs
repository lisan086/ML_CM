using Microsoft.EntityFrameworkCore;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Deliverables;
using ML.PCM.Entities.Payments;
using ML.PCM.Entities.Procurements;
using ML.PCM.Managers.Contracts;
using ML.PCM.Services.Dtos.Contracts;
using ML.PCM.Services.Dtos.DecisionDocuments;
using ML.PCM.Services.Dtos.Deliverables;
using ML.PCM.Services.Dtos.Payments;
using ML.PCM.Services.Dtos.Procurements;
using ML.PCM.Services.Projects;
using System.Linq.Dynamic.Core;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Contract = ML.PCM.Entities.Contracts.Contract;

namespace ML.PCM.Services.Contracts;

public class ContractAppService : ApplicationService, IContractAppService
{
    private readonly IRepository<DecisionDocument, Guid> _decisionRepository;
    private readonly IRepository<Contract, Guid> _repository;
    private readonly ContractManager _manager; // 注入 Manager

    private readonly IRepository<Payment, Guid> _paymentRepository;
    private readonly IRepository<Deliverable, Guid> _deliverableRepository;

    public ContractAppService(
        IRepository<Contract, Guid> repository,
        IRepository<DecisionDocument, Guid> decisionRepository,
        IRepository<Payment, Guid> paymentRepository,
        IRepository<Deliverable, Guid> deliverableRepository,
        ContractManager contractManager)
    {
        _repository = repository;
        _manager = contractManager;
        _decisionRepository = decisionRepository;

        _paymentRepository = paymentRepository;
        _deliverableRepository = deliverableRepository;
    }

    // 获取单个详情
    public async Task<ContractDto> GetAsync(Guid id)
    {
        var queryable = await _repository.GetQueryableAsync();
        var entity = await AsyncExecuter.FirstOrDefaultAsync(
            queryable
                .Include(x => x.Project)
                .Include(x => x.RelatedDecisions)
                .Include(x => x.Procurement)
                .Where(x => x.Id == id)
        );

        // 如果找不到抛出异常
        if (entity == null) throw new EntityNotFoundException(typeof(Contract), id);
        // 2. 基础映射
        var dto = ObjectMapper.Map<Contract, ContractDto>(entity);

        // 3. 显式查询支付记录
        // 直接去 AppPayments 表查，不经过 Contract 实体，这样绝对能查到数据！
        var payments = await _paymentRepository.GetListAsync(x => x.ContractId == id);
        if (payments.Any())
        {
            dto.Payments = ObjectMapper.Map<List<Payment>, List<PaymentDto>>(payments);
            dto.TotalPaidAmount = dto.Payments.Sum(x => x.AmountPaid);
        }

        // 4. 显式查询成果文件
        var deliverables = await _deliverableRepository.GetListAsync(x => x.ContractId == id);
        if (deliverables.Any())
        {
            dto.Deliverables = ObjectMapper.Map<List<Deliverable>, List<DeliverableDto>>(deliverables);
        }

        // 5. 决策文件 (如果你发现决策文件也丢了，也可以这样查，不过通常多对多Include比较稳)
        // dto.RelatedDecisions = ...


        return dto;
    }

    // 获取分页列表
    public async Task<PagedResultDto<ContractDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        // 1. 获取 Queryable
        var queryable = await _repository.GetQueryableAsync();

        // 2. 构造查询 (注意：这里我们暂时不 Include Payments 和 Deliverables，改为手动查)
        var query = queryable
            .Include(x => x.Project)
            .Include(x => x.RelatedDecisions)
            .Include(x => x.Procurement)
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        // 3. 执行查询
        var entities = await AsyncExecuter.ToListAsync(query);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        // 4. 基础映射 (此时 dtos 里的 Payments 和 Deliverables 是空的)
        var dtos = ObjectMapper.Map<List<Contract>, List<ContractDto>>(entities);

        // 5. 解决 Include 失效问题，同时保证性能
        if (dtos.Any())
        {
            // 提取当前页所有合同的 ID
            var contractIds = dtos.Select(x => x.Id).ToList();

            // A. 批量查询支付记录 (一次 SQL 查出所有相关支付)
            var allPayments = await _paymentRepository.GetListAsync(x => contractIds.Contains(x.ContractId));

            // B. 批量查询成果文件 (一次 SQL 查出所有相关成果)
            var allDeliverables = await _deliverableRepository.GetListAsync(x => contractIds.Contains(x.ContractId));

            // C. 在内存中进行配对
            foreach (var dto in dtos)
            {
                // 筛选属于当前合同的支付记录
                var relatedPayments = allPayments.Where(p => p.ContractId == dto.Id).ToList();
                if (relatedPayments.Any())
                {
                    // 手动映射 List
                    dto.Payments = ObjectMapper.Map<List<Payment>, List<PaymentDto>>(relatedPayments);
                    // 计算统计金额 (列表页非常需要这个)
                    dto.TotalPaidAmount = relatedPayments.Sum(x => x.AmountPaid);
                }

                // 筛选属于当前合同的成果文件
                var relatedDeliverables = allDeliverables.Where(d => d.ContractId == dto.Id).ToList();
                if (relatedDeliverables.Any())
                {
                    dto.Deliverables = ObjectMapper.Map<List<Deliverable>, List<DeliverableDto>>(relatedDeliverables);
                }
            }
        }

        // 6. 映射并返回
        return new PagedResultDto<ContractDto>(totalCount, dtos);
    }

    public async Task<PagedResultWithAggregationDto<ContractDto>> GetListWithAggregationAsync(PagedAndSortedResultRequestDto input)
    {
        // 1. 获取 Queryable
        var queryable = await _repository.GetQueryableAsync();

        // 2.【第一步：计算全局聚合数据】(在分页之前执行)
        // A. 计算合同总金额
        var totalContractAmount = await AsyncExecuter.SumAsync(queryable, x => x.ContractAmount);
        // B. 计算累计已付总额 (关键修改！！！)
        // 既然 Include 无效，我们直接查 Payment 表。
        // 逻辑：计算所有 Payment 的金额，前提是 Payment.ContractId 必须在上面的 queryable (合同列表) 里。

        // 2.1 拿到 Payment 的查询对象
        var paymentQueryable = await _paymentRepository.GetQueryableAsync();
        // 2.2 构造子查询：提取符合条件的合同 ID
        var validContractIds = queryable.Select(c => c.Id);
        // 2.3 直接在数据库计算 Sum (生成 WHERE ContractId IN (...) 的 SQL)
        // 这种写法不用把数据拉到内存，性能最高，且不受 Include 失败的影响
        var totalPaidAmount = await AsyncExecuter.SumAsync(
            paymentQueryable.Where(p => validContractIds.Contains(p.ContractId)),
            p => p.AmountPaid
        );

        //var totalPaidAmount = await AsyncExecuter.SumAsync(queryable
        //    .Include(x => x.Payments)
        //    .SelectMany(c => c.Payments)
        //    ,p => p.AmountPaid
        //);

        // 3.【第二步：查询当前页列表数据】
        // A. 构造分页和排序查询
        // 这里 Include 基础关联，但 Payment/Deliverable 我们稍后手动查
        var pagedQuery = queryable
            .Include(x => x.Project)
            .Include(x => x.Procurement)
            .Include(x => x.RelatedDecisions)
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "CreationTime DESC" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        // B. 执行数据库查询
        var entities = await AsyncExecuter.ToListAsync(pagedQuery);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        // C. 基础映射 (Entity -> DTO)
        var dtos = ObjectMapper.Map<List<Contract>, List<ContractDto>>(entities);

        // 4.【第三步：手动批量补全子列表】(解决 Include 失效问题)
        if (dtos.Any())
        {
            // 提取当前页所有合同 ID
            var contractIds = dtos.Select(x => x.Id).ToList();

            // 批量查支付
            var relatedPayments = await _paymentRepository.GetListAsync(p => contractIds.Contains(p.ContractId));
            // 批量查成果
            var relatedDeliverables = await _deliverableRepository.GetListAsync(d => contractIds.Contains(d.ContractId));

            // 内存配对
            foreach (var dto in dtos)
            {
                // 填充支付信息
                var myPayments = relatedPayments.Where(p => p.ContractId == dto.Id).ToList();
                if (myPayments.Any())
                {
                    dto.Payments = ObjectMapper.Map<List<Payment>, List<PaymentDto>>(myPayments);
                    // 计算单行数据的“已付额”
                    dto.TotalPaidAmount = myPayments.Sum(p => p.AmountPaid);
                }

                // 填充成果信息
                var myDeliverables = relatedDeliverables.Where(d => d.ContractId == dto.Id).ToList();
                if (myDeliverables.Any())
                {
                    dto.Deliverables = ObjectMapper.Map<List<Deliverable>, List<DeliverableDto>>(myDeliverables);
                }

                // 填充决策信息 (如果 Include 生效，这里应该已经有了；如果为空，也可以这里手动补)
                // var entity = entities.First(e => e.Id == dto.Id);
                // if (entity.RelatedDecisions.Any()) ...
            }
        }

        // 5.【第四步：组装最终结果】
        return new PagedResultWithAggregationDto<ContractDto>(totalCount, dtos)
        {
            TotalContractAmount = totalContractAmount,
            TotalPaidAmount = totalPaidAmount
            // TotalOutstandingAmount 会自动计算
        };
    }

    // 创建
    //[Authorize(PCMPermissions.Contracts.Create)]
    public async Task<ContractDto> CreateAsync(CreateUpdateContractDto input)
    {
        // 校验：虽然前端选了对象，但我们要防一手空指针
        if (input.Project == null)
        {
            throw new UserFriendlyException("必须关联一个项目");
        }

        // 1. 使用 Manager 创建带有自动编号的实体
        var entity = await _manager.CreateAsync(input.ProjectId, input.Name);
        // 2. 将 DTO 中其他的字段 (TotalInvestment, Manager等) 映射到实体上
        // 注意：我们需要配置 Mapper 不覆盖 ContractCode 和 Id
        ObjectMapper.Map(input, entity);

        // 处理关联决策文件
        if (input.RelatedDecisionIds != null && input.RelatedDecisionIds.Any())
        {
            var decisions = await _decisionRepository.GetListAsync(d => input.RelatedDecisionIds.Contains(d.Id));
            entity.RelatedDecisions = decisions;
        }

        // 3. 保存到数据库
        await _repository.InsertAsync(entity);

        // 手动回填返回
        var dto = ObjectMapper.Map<Contract, ContractDto>(entity);
        dto.Project = input.Project;
        dto.ProjectId = input.Project.Id;
        dto.Procurement = input.Procurement;
        dto.ProcurementId = input.Procurement.Id;
        // 4. 返回 DTO
        return dto;
    }

    // 更新
    //[Authorize(PCMPermissions.Contracts.Edit)]
    public async Task<ContractDto> UpdateAsync(Guid id, CreateUpdateContractDto input)
    {
        // 1. 获取实体 (这里保留 Include 也没事，或者去掉 Include 也可以
        // 必须使用 GetQueryableAsync + Include 来加载关联项目，
        // 否则最后一步映射 DTO 时，entity.Project 为空会导致崩溃。
        var queryable = await _repository.GetQueryableAsync();
        var entity = await AsyncExecuter.FirstOrDefaultAsync(
            queryable.Include(x => x.Project)
                .Include(x => x.Procurement)
                .Include(x => x.RelatedDecisions)
                .Where(x => x.Id == id)
        );

        if (entity == null)
        {
            throw new EntityNotFoundException(typeof(Procurement), id);
        }

        // 映射表单修改 (Mapperly 会自动忽略 Id 和 DocumentCode)
        // 此时 input.Project 可能有值，但我们在 Mapper 里配置了忽略 input.Project 对象映射，
        // 只会映射 ProjectId，这是正确的。
        // 2. 更新字段 (ProjectId 会被更新)
        ObjectMapper.Map(input, entity);

        // 更新多对多关系 (全量替换)
        entity.RelatedDecisions.Clear();
        if (input.RelatedDecisionIds != null && input.RelatedDecisionIds.Any())
        {
            var decisions = await _decisionRepository.GetListAsync(d => input.RelatedDecisionIds.Contains(d.Id));
            foreach (var d in decisions) entity.RelatedDecisions.Add(d);
        }

        // 3. 保存
        await _repository.UpdateAsync(entity);

        // 4. 映射返回结果
        var dto = ObjectMapper.Map<Contract, ContractDto>(entity);

        // ✅5. 手动修正返回的 Project 信息
        // 如果 input 里传了最新的 Project 对象，直接赋给 dto
        // 这样能确保前端拿到的返回值里，项目名称是更新后的（比如 "项目 B"）
        if (input.Project != null)
        {
            dto.Project = input.Project;
            dto.ProjectId = input.Project.Id;
        }

        if (input.Procurement != null)
        {
            dto.Procurement = input.Procurement;
            dto.ProcurementId = input.Procurement.Id;
        }

        return dto;
    }

    // 删除
    //[Authorize(PCMPermissions.Contracts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}

