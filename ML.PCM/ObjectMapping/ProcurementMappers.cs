using ML.PCM.Entities.Procurements;
using ML.PCM.Services.Dtos.Procurements;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace ML.PCM.ObjectMapping;

// 1. 实体 -> DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProcurementToProcurementDtoMapper : MapperBase<Procurement, ProcurementDto>
{
    public override partial ProcurementDto Map(Procurement source);
    public override partial void Map(Procurement source, ProcurementDto destination);
}

// 2. 表单 DTO -> 实体
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdateProcurementDtoToProcurementMapper : MapperBase<CreateUpdateProcurementDto, Procurement>
{
    public override Procurement Map(CreateUpdateProcurementDto source)
    {
        throw new NotSupportedException("Procurement实体必须通过 ProcurementManager 创建以生成编号，请勿直接使用此映射创建新实例。请使用 Map(source, destination) 进行属性填充。");
    }

    // 更新实例：忽略不需要映射的字段
    [MapperIgnoreTarget(nameof(Procurement.DocumentCode))]
    [MapperIgnoreTarget(nameof(Procurement.Id))]
    [MapperIgnoreTarget(nameof(Procurement.Project))]          // ✅ 忽略复杂对象
    [MapperIgnoreTarget(nameof(Procurement.RelatedDecisions))] // ✅ 忽略多对多集合（稍后手动处理）    [MapperIgnoreTarget(nameof(Procurement.Project))]     // 忽略复杂对象 (只映射 ProjectId)
    public override partial void Map(CreateUpdateProcurementDto source, Procurement destination);
}

// 3. DTO -> 表单 DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProcurementDtoToCreateUpdateProcurementDtoMapper : MapperBase<ProcurementDto, CreateUpdateProcurementDto>
{
    public override partial CreateUpdateProcurementDto Map(ProcurementDto source);
    public override partial void Map(ProcurementDto source, CreateUpdateProcurementDto destination);
}