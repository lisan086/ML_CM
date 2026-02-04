using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using ML.PCM.Entities.Projects;
using ML.PCM.Services.Dtos.Projects;

namespace ML.PCM.ObjectMapping;
// =========================================================
// Project 映射定义
// =========================================================

// 1. 实体转 DTO (用于列表展示)
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectToProjectDtoMapper : MapperBase<Project, ProjectDto>
{
    public override partial ProjectDto Map(Project source);

    public override partial void Map(Project source, ProjectDto destination);
}

// 2. 表单 DTO 转实体 (用于创建和更新保存)
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class CreateUpdateProjectDtoToProjectMapper : MapperBase<CreateUpdateProjectDto, Project>
{
    // Mapperly 会自动匹配同名属
    public override Project Map(CreateUpdateProjectDto source)
    {
        throw new NotSupportedException("Project实体必须通过 ProjectManager 创建以生成编号，请勿直接使用此映射创建新实例。请使用 Map(source, destination) 进行属性填充。");
    }

    [MapperIgnoreTarget(nameof(Project.ProjectCode))] // 忽略编号，防止被覆盖
    [MapperIgnoreTarget(nameof(Project.Id))]          // 忽略ID
    public override partial void Map(CreateUpdateProjectDto source, Project destination);
}

// 3. DTO 转 表单 DTO (用于点击"编辑"时，将数据显示在表单上)
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProjectDtoToCreateUpdateProjectDtoMapper : MapperBase<ProjectDto, CreateUpdateProjectDto>
{
    public override partial CreateUpdateProjectDto Map(ProjectDto source);

    public override partial void Map(ProjectDto source, CreateUpdateProjectDto destination);
}


