using ML.PCM.Services.Dtos.Contracts;
using ML.PCM.Services.Dtos.Projects;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ML.PCM.Services.Projects;

public interface IContractAppService :
    ICrudAppService<
        ContractDto,
        Guid,
        PagedAndSortedResultRequestDto, // ABP 默认的分页排序查询参数
        CreateUpdateContractDto>
{
    // 新增这个接口定义
    Task<PagedResultWithAggregationDto<ContractDto>> GetListWithAggregationAsync(PagedAndSortedResultRequestDto input);
}