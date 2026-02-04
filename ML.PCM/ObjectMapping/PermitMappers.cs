using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Permits;
using ML.PCM.Services.Dtos.Permits;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace ML.PCM.ObjectMapping;

// 1. 实体 -> DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class PermitToPermitDtoMapper : MapperBase<Permit, PermitDto>
{
    public override partial PermitDto Map(Permit source);
    public override partial void Map(Permit source, PermitDto destination);
}

// 2. 表单 DTO -> 实体
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdatePermitDtoToPermitMapper : MapperBase<CreateUpdatePermitDto, Permit>
{
    public override  Permit Map(CreateUpdatePermitDto source)
    {
        throw new NotSupportedException("Permit实体必须通过 PermitManager 创建以生成编号，请勿直接使用此映射创建新实例。请使用 Map(source, destination) 进行属性填充。");
    }

    // 更新实例：忽略不需要映射的字段
    [MapperIgnoreTarget(nameof(Permit.DocumentCode))] // 忽略编号 (Manager生成，不可改)
    [MapperIgnoreTarget(nameof(Permit.Id))]          // 忽略主键
    [MapperIgnoreTarget(nameof(Permit.Project))]     // 忽略复杂对象 (只映射 ProjectId)
    public override partial void Map(CreateUpdatePermitDto source, Permit destination);
}

// 3. DTO -> 表单 DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class PermitDtoToCreateUpdatePermitDtoMapper : MapperBase<PermitDto, CreateUpdatePermitDto>
{
    public override partial CreateUpdatePermitDto Map(PermitDto source);
    public override partial void Map(PermitDto source, CreateUpdatePermitDto destination);
}