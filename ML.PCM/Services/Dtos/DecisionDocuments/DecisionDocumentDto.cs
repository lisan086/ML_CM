using ML.PCM.Enums;
using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;
using ML.PCM.Entities.Projects;
using ML.PCM.Services.Dtos.Projects;

namespace ML.PCM.Services.Dtos.DecisionDocuments;

public class DecisionDocumentDto : AuditedEntityDto<Guid>
{
    [Description("文件编号")]
    public string DocumentCode { get; set; }

    [Description("文件名称")]
    public string Name { get; set; }

    [Description("文件分类")]
    public DecisionCategory? Category { get; set; }

    [Description("文号")]
    public string? ReferenceNumber { get; set; }

    [Description("成文日期")]
    public DateTime? DocumentDate { get; set; }

    [Description("备注")]
    public string? Remarks { get; set; }

    [Description("关联的项目ID")]
    public Guid ProjectId { get; set; } // 外键

    [Description("关联的项目")]
    public ProjectDto? Project { get; set; } // 导航属性
}