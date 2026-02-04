using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Deliverables;
using ML.PCM.Entities.Payments;
using ML.PCM.Entities.Procurements;
using ML.PCM.Entities.Projects;
using ML.PCM.Enums;
using ML.PCM.Services.Dtos.DecisionDocuments;
using ML.PCM.Services.Dtos.Deliverables;
using ML.PCM.Services.Dtos.Payments;
using ML.PCM.Services.Dtos.Procurements;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Services.Dtos.Projects;
using Volo.Abp.Application.Dtos;

namespace ML.PCM.Services.Dtos.Contracts;

public class ContractDto : AuditedEntityDto<Guid>
{
    [Description("合同编号")]
    public string DocumentCode { get; set; } = string.Empty;

    [Description("合同分类")]
    public ContractCategory? Category { get; set; } // <-- 使用新的枚举类型

    [Description("合同名称/摘要")]
    public string Name { get; set; } = string.Empty;

    [Description("文号")]
    public string? ReferenceNumber { get; set; }

    [Description("签订日期")]
    public DateTime? SigningDate { get; set; }

    [Description("合同金额")]
    public decimal ContractAmount { get; set; }

    [Description("累计已付额")]
    public decimal TotalPaidAmount { get; set; } // 由 AutoMapper 实时计算

    [Description("乙方单位")]
    public string? PartyB { get; set; } // 乙方

    [Description("乙方联系人")]
    public string? ContactPerson { get; set; }

    [Description("是否虚拟合同")]
    public bool IsVirtual { get; set; }

    [Description("备注")]
    public string? Remarks { get; set; }


    [Required(ErrorMessage = "必须关联到一个项目。")]
    [Description("关联的项目ID")]
    public Guid ProjectId { get; set; } // 外键

    [Description("关联的项目")]
    public virtual ProjectDto? Project { get; set; }// 导航属性

    [Description("关联的招采文件ID")]
    public Guid? ProcurementId { get; set; } // 外键 (可空)

    [Description("关联的招采文件")]
    public virtual ProcurementDto? Procurement { get; set; } // 导航属性

    [Description("关联的资金支付")]
    public List<PaymentDto> Payments { get; set; } = new();
    [Description("关联的决策文件")]
    public List<DecisionDocumentDto> RelatedDecisions { get; set; } = new();
    [Description("关联的成果文件")]
    public List<DeliverableDto> Deliverables { get; set; } = new();

}