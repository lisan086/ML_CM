using ML.PCM.Services.Dtos.Deliverables;
using ML.PCM.Services.Dtos.Procurements;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ML.PCM.Services.Deliverables;

public interface IDeliverableAppService :
    ICrudAppService<
        DeliverableDto,
        Guid,
        PagedAndSortedResultRequestDto, // ABP 默认的分页排序查询参数
        CreateUpdateDeliverableDto>
{
}
