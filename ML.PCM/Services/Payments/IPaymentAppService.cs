using ML.PCM.Services.Dtos.Payments;
using ML.PCM.Services.Dtos.Permits;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace ML.PCM.Services.Payments;

public interface IPaymentAppService :
    ICrudAppService<
        PaymentDto,
        Guid,
        PagedAndSortedResultRequestDto, // ABP 默认的分页排序查询参数
        CreateUpdatePaymentDto>
{
}
