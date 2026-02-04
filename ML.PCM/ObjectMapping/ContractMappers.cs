using ML.PCM.Entities.Contracts;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Deliverables;
using ML.PCM.Entities.Payments;
using ML.PCM.Services.Dtos.Contracts;
using ML.PCM.Services.Dtos.DecisionDocuments;
using ML.PCM.Services.Dtos.Deliverables;
using ML.PCM.Services.Dtos.Payments;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace ML.PCM.ObjectMapping;

// 1. 实体 -> DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ContractToContractDtoMapper : MapperBase<Contract, ContractDto>
{
    // 主映射方法
    [MapProperty(nameof(Contract.Project), nameof(ContractDto.Project))]
    [MapProperty(nameof(Contract.Procurement), nameof(ContractDto.Procurement))]
    public override partial ContractDto Map(Contract source);
    public override partial void Map(Contract source, ContractDto destination);

    // 1. 映射支付记录
    [MapProperty(nameof(Payment.Contract), nameof(PaymentDto.Contract))]
    public partial PaymentDto Map(Payment source);

    // 2. 映射成果文件
    [MapProperty(nameof(Deliverable.Contract), nameof(DeliverableDto.Contract))]
    public partial DeliverableDto Map(Deliverable source);

    // 3. 映射决策文件 (之前能显示是因为可能用了其他地方的配置，最好这里也显式加上)
    [MapProperty(nameof(DecisionDocument.Project), nameof(DecisionDocumentDto.Project))]
    public partial DecisionDocumentDto Map(DecisionDocument source);

}

// 2. 表单 DTO -> 实体
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdateContractDtoToContractMapper : MapperBase<CreateUpdateContractDto, Contract>
{
    public override Contract Map(CreateUpdateContractDto source)
    {
        throw new NotSupportedException("必须通过 ContractManager 创建。");
    }

    [MapperIgnoreTarget(nameof(Contract.DocumentCode))]
    [MapperIgnoreTarget(nameof(Contract.Id))]
    [MapperIgnoreTarget(nameof(Contract.Project))]           
    [MapperIgnoreTarget(nameof(Contract.Procurement))]       
    [MapperIgnoreTarget(nameof(Contract.RelatedDecisions))]  
    [MapperIgnoreTarget(nameof(Contract.Payments))]
    [MapperIgnoreTarget(nameof(Contract.Deliverables))]
    public override partial void Map(CreateUpdateContractDto source, Contract destination);
}

// 3. DTO -> 表单 DTO (编辑回显)
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ContractDtoToCreateUpdateContractDtoMapper : MapperBase<ContractDto, CreateUpdateContractDto>
{
    public override partial CreateUpdateContractDto Map(ContractDto source);
    public override partial void Map(ContractDto source, CreateUpdateContractDto destination);
}
