using ML.PCM.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Services.Dtos.Contracts;
using Volo.Abp.Application.Dtos;

namespace ML.PCM.Services.Dtos.Deliverables;

public class DeliverableDto : AuditedEntityDto<Guid>
{
    [Description("文件编号")]
    public string DocumentCode { get; set; }

    [Description("成果分类")]
    public DeliverableCategory? Category { get; set; }

    [Description("成果名称")]
    public string Name { get; set; }

    [Description("摘要")]
    public string? Summary { get; set; }

    [Description("文号")]
    public string? ReferenceNumber { get; set; }

    [Description("审批日期")]
    public DateTime? ApprovalDate { get; set; }

    [Required(ErrorMessage = "必须关联到一个合同。")]
    [Description("合同ID")]
    public Guid ContractId { get; set; } // 外键

    [Description("关联的合同")]
    public ContractDto? Contract { get; set; } // 导航属性
}