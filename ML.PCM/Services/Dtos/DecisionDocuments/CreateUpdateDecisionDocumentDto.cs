using ML.PCM.Entities.Projects;
using ML.PCM.Enums;
using ML.PCM.Services.Dtos.Projects;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML.PCM.Services.Dtos.DecisionDocuments;

public class CreateUpdateDecisionDocumentDto
{
    [Description("文件编号")]
    public string DocumentCode { get; set; }

    [Required]
    [StringLength(200)]
    [Description("文件名称")]
    public string Name { get; set; }

    [Description("文件分类")]
    public DecisionCategory? Category { get; set; }

    [Description("文号")]
    [StringLength(100)]
    public string? ReferenceNumber { get; set; }

    [Description("成文日期")]
    public DateTime? DocumentDate { get; set; }

    [Description("备注")]
    public string? Remarks { get; set; }

    [Description("关联的项目ID")]
    public Guid ProjectId { get; set; } // 外键

    [Description("关联的项目")]
    public virtual ProjectDto Project { get; set; } = null!; // 导航属性
}