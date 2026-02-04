using System;
using ML.PCM.Services.Dtos.Projects;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ML.PCM.Services.Projects;

public interface IProjectAppService :
    ICrudAppService<
        ProjectDto,
        Guid,
        PagedAndSortedResultRequestDto, // ABP 默认的分页排序查询参数
        CreateUpdateProjectDto>
{
}