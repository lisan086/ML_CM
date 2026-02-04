using ML.PCM.Services.Dtos.DecisionDocuments;
using ML.PCM.Services.Dtos.Permits;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ML.PCM.Services.Permits;

public interface IPermitAppService :
    ICrudAppService<
        PermitDto,
        Guid,
        PagedAndSortedResultRequestDto, // ABP 默认的分页排序查询参数
        CreateUpdatePermitDto>
{
}

