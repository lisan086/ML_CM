

using ML.PCM.Services.Dtos.Deliverables;
using ML.PCM.Services.Dtos.Permits;
using ML.PCM.Services.Dtos.Procurements;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ML.PCM.Services.Procurements;

public interface IProcurementAppService :
    ICrudAppService<
        ProcurementDto,
        Guid,
        PagedAndSortedResultRequestDto, // ABP 默认的分页排序查询参数
        CreateUpdateProcurementDto>
{
}

