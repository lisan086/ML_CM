using ML.PCM.Enums;
using ML.PCM.Services.Dtos.Projects;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML.PCM.Services.Dtos.Permits;

public class CreateUpdatePermitDto
{
    [Description("文件编号")]
    public string DocumentCode { get; set; }

    [StringLength(200)]
    [Description("文件名称")]
    public string Name { get; set; }

    [Description("文件分类")]
    public PermitCategory? Category { get; set; }

    [Description("文号")]
    [StringLength(100)]
    public string? ReferenceNumber { get; set; }

    [Description("批复日期")]
    public DateTime? ApprovalDate { get; set; }

    [Description("摘要")]
    public string? Summary { get; set; }

    [Description("备注")]
    public string? Remarks { get; set; }

    [Required]
    [Description("关联的项目ID")]
    public Guid ProjectId { get; set; }

    [Description("关联的项目")]
    public virtual ProjectDto Project { get; set; } = null!;
}