using Microsoft.AspNetCore.Authorization;
using ML.PCM.Entities.Projects;
using ML.PCM.Managers.Projects;
using ML.PCM.Permissions;
using ML.PCM.Services.Dtos.Projects;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace ML.PCM.Services.Projects;

//[Authorize(PCMPermissions.Projects.Default)]
public class ProjectAppService : ApplicationService, IProjectAppService
{
    private readonly IRepository<Project, Guid> _repository;
    private readonly ProjectManager _projectManager; // 注入 Manager

    public ProjectAppService(
        IRepository<Project, Guid> repository, 
        ProjectManager projectManager)
    {
        _repository = repository;
        _projectManager = projectManager;
    }

    // 获取单个详情
    public async Task<ProjectDto> GetAsync(Guid id)
    {
        var entity = await _repository.GetAsync(id);
        return ObjectMapper.Map<Project, ProjectDto>(entity);
    }

    // 获取分页列表
    public async Task<PagedResultDto<ProjectDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        // 1. 获取 Queryable
        var queryable = await _repository.GetQueryableAsync();
        
        // 2. 应用排序 (如果没有指定排序，默认按 Name 排序，你可以改为 CreationTime DESC)
        var query = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "Name" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        // 3. 执行查询
        var entitys = await AsyncExecuter.ToListAsync(query);

        // 4. 获取总数 (用于分页)
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        // 5. 映射并返回
        return new PagedResultDto<ProjectDto>(
            totalCount,
            ObjectMapper.Map<List<Project>, List<ProjectDto>>(entitys)
        );
    }

    // 创建
    //[Authorize(PCMPermissions.Projects.Create)]
    public async Task<ProjectDto> CreateAsync(CreateUpdateProjectDto input)
    {
        // 1. 使用 Manager 创建带有自动编号的实体
        var project = await _projectManager.CreateAsync(input.Name);
        // 2. 将 DTO 中其他的字段 (TotalInvestment, Manager等) 映射到实体上
        // 注意：我们需要配置 Mapper 不覆盖 ProjectCode 和 Id
        ObjectMapper.Map(input, project);

        // 3. 保存到数据库
        await _repository.InsertAsync(project);
        // 4. 返回 DTO
        return ObjectMapper.Map<Project, ProjectDto>(project);
    }

    // 更新
    //[Authorize(PCMPermissions.Projects.Edit)]
    public async Task<ProjectDto> UpdateAsync(Guid id, CreateUpdateProjectDto input)
    {
        var project = await _repository.GetAsync(id);
        ObjectMapper.Map(input, project);
        await _repository.UpdateAsync(project);
        return ObjectMapper.Map<Project, ProjectDto>(project);
    }

    // 删除
    //[Authorize(PCMPermissions.Projects.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }
}

