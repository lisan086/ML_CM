using Volo.Abp.Application.Dtos;

namespace ML.PCM.Services.Dtos.Contracts;

/// <summary>
/// 泛型 DTO，继承自 ABP 的 PagedResultDto
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedResultWithAggregationDto<T> : PagedResultDto<T>
{
    /// <summary>
    /// 总金额
    /// </summary>
    public decimal TotalContractAmount { get; set; }

    /// <summary>
    /// 已付金额
    /// </summary>
    public decimal TotalPaidAmount { get; set; }

    /// <summary>
    /// 欠付金额
    /// </summary>
    public decimal TotalOutstandingAmount => TotalContractAmount - TotalPaidAmount;

    public PagedResultWithAggregationDto(long totalCount, IReadOnlyList<T> items)
        : base(totalCount, items)
    {
    }
}
