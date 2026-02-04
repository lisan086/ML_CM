using ML.PCM.Enums;
using ML.PCM.Services.Dtos.DecisionDocuments;
using ML.PCM.Services.Dtos.Projects;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Entities.Projects;
using Volo.Abp.Application.Dtos;

namespace ML.PCM.Services.Dtos.Procurements;

public class ProcurementDto : AuditedEntityDto<Guid>
{

    [Required(ErrorMessage = "文件编号是必需的。")]
    [StringLength(100)]
    [Description("文件编号")]
    public string DocumentCode { get; set; } = string.Empty;

    [Description("招采分类")]
    public ProcurementCategory? Category { get; set; }

    [Description("摘要")]
    public string? Summary { get; set; }

    [StringLength(100)]
    [Description("文号")]
    public string? ReferenceNumber { get; set; }

    [Description("公告日期")]
    public DateTime? AnnouncementDate { get; set; }

    [Description("控制价")]
    public decimal? ControlPrice { get; set; }

    [Description("中标金额")]
    public decimal? WinningBidAmount { get; set; }

    [Description("决定日期")]
    public DateTime? DecisionDate { get; set; }

    [Description("开标时间")]
    public DateTime? BidOpeningDate { get; set; }

    [Description("中标时间")]
    public DateTime? BidWinningDate { get; set; }

    [StringLength(200)]
    [Description("中标单位")]
    public string? WinningBidder { get; set; }

    [StringLength(100)]
    [Description("联系人")]
    public string? ContactPerson { get; set; }

    [Description("备注")]
    public string? Remarks { get; set; }

    [Required(ErrorMessage = "必须关联到一个项目。")]
    [Description("项目ID")]
    public Guid ProjectId { get; set; } // 外键

    [Description("关联的项目")]
    public ProjectDto? Project { get; set; }// 导航属性

    // 列表页可能需要显示关联了几个决策文件
    public List<DecisionDocumentDto> RelatedDecisions { get; set; } = new();

}