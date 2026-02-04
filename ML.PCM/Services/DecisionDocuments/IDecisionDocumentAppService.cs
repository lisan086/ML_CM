using ML.PCM.Services.Dtos.DecisionDocuments;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ML.PCM.Services.DecisionDocuments;

public interface IDecisionDocumentAppService :
    ICrudAppService<
        DecisionDocumentDto,
        Guid,
        PagedAndSortedResultRequestDto, // ABP 默认的分页排序查询参数
        CreateUpdateDecisionDocumentDto>
{

}