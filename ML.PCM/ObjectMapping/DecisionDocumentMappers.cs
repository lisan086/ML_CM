using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Services.Dtos.DecisionDocuments;

namespace ML.PCM.ObjectMapping;

// 1. 实体 -> DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DecisionDocumentToDecisionDocumentDtoMapper : MapperBase<DecisionDocument, DecisionDocumentDto>
{
    public override partial DecisionDocumentDto Map(DecisionDocument source);
    public override partial void Map(DecisionDocument source, DecisionDocumentDto destination);
}

// 2. 表单 DTO -> 实体
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdateDecisionDocumentDtoToDecisionDocumentMapper : MapperBase<CreateUpdateDecisionDocumentDto, DecisionDocument>
{
    public override DecisionDocument Map(CreateUpdateDecisionDocumentDto source)
    {
        throw new NotSupportedException("DecisionDocument实体必须通过 DecisionDocumentManager 创建以生成编号，请勿直接使用此映射创建新实例。请使用 Map(source, destination) 进行属性填充。");
    }

    // 更新实例：忽略不需要映射的字段
    [MapperIgnoreTarget(nameof(DecisionDocument.DocumentCode))] // 忽略编号 (Manager生成，不可改)
    [MapperIgnoreTarget(nameof(DecisionDocument.Id))]          // 忽略主键
    [MapperIgnoreTarget(nameof(DecisionDocument.Project))]     // 忽略复杂对象 (只映射 ProjectId)
    public override partial void Map(CreateUpdateDecisionDocumentDto source, DecisionDocument destination);
}

// 3. DTO -> 表单 DTO
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class DecisionDocumentDtoToCreateUpdateDecisionDocumentDtoMapper : MapperBase<DecisionDocumentDto, CreateUpdateDecisionDocumentDto>
{
    public override partial CreateUpdateDecisionDocumentDto Map(DecisionDocumentDto source);
    public override partial void Map(DecisionDocumentDto source, CreateUpdateDecisionDocumentDto destination);
}
