using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using ML.PCM.Entities.Payments;
using ML.PCM.Services.Dtos.Payments;

namespace ML.PCM.ObjectMapping;

// 1. 实体 -> DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class PaymentToPaymentDtoMapper : MapperBase<Payment, PaymentDto>
{
    public override partial PaymentDto Map(Payment source);
    public override partial void Map(Payment source, PaymentDto destination);
}

// 2. 表单 DTO -> 实体
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdatePaymentDtoToPaymentMapper : MapperBase<CreateUpdatePaymentDto, Payment>
{
    public override  Payment Map(CreateUpdatePaymentDto source)
    {
        throw new NotSupportedException("Payment实体必须通过 PaymentManager 创建以生成编号，请勿直接使用此映射创建新实例。请使用 Map(source, destination) 进行属性填充。");

    }

    [MapperIgnoreTarget(nameof(Payment.DocumentCode))]
    [MapperIgnoreTarget(nameof(Payment.Id))]
    [MapperIgnoreTarget(nameof(Payment.Contract))]
    public override partial void Map(CreateUpdatePaymentDto source, Payment destination);
}

// 3. DTO -> 表单 DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class PaymentDtoToCreateUpdatePaymentDtoMapper : MapperBase<PaymentDto, CreateUpdatePaymentDto>
{
    public override partial CreateUpdatePaymentDto Map(PaymentDto source);
    public override partial void Map(PaymentDto source, CreateUpdatePaymentDto destination);
}