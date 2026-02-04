using ML.PCM.Entities.Deliverables;
using ML.PCM.Entities.Procurements;
using ML.PCM.Services.Dtos.Deliverables;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace ML.PCM.ObjectMapping;

// 1. 实体 -> DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DeliverableToDeliverableDtoMapper : MapperBase<Deliverable, DeliverableDto>
{
    public override partial DeliverableDto Map(Deliverable source);
    public override partial void Map(Deliverable source, DeliverableDto destination);
}

// 2. 表单 DTO -> 实体
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdateDeliverableDtoToDeliverableMapper : MapperBase<CreateUpdateDeliverableDto, Deliverable>
{
    public override  Deliverable Map(CreateUpdateDeliverableDto source)
    {
        throw new NotSupportedException("Deliverable实体必须通过 DeliverableManager 创建以生成编号，请勿直接使用此映射创建新实例。请使用 Map(source, destination) 进行属性填充。");
    }
    // 更新实例：忽略不需要映射的字段
    [MapperIgnoreTarget(nameof(Deliverable.DocumentCode))]
    [MapperIgnoreTarget(nameof(Deliverable.Id))]
    [MapperIgnoreTarget(nameof(Deliverable.Contract))]          // ✅ 必须忽略，只映射 ContractId
    public override partial void Map(CreateUpdateDeliverableDto source, Deliverable destination);
}

// 3. DTO -> 表单 DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DeliverableDtoToCreateUpdateDeliverableDtoMapper : MapperBase<DeliverableDto, CreateUpdateDeliverableDto>
{
    public override partial CreateUpdateDeliverableDto Map(DeliverableDto source);
    public override partial void Map(DeliverableDto source, CreateUpdateDeliverableDto destination);
}