using ML.PCM.Entities.DecisionDocuments;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Entities.Contracts;
using ML.PCM.Entities.Projects;
using ML.PCM.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace ML.PCM.Entities.Procurements
{
    /// <summary>
    /// 招采信息表
    /// </summary>
    public class Procurement : AuditedAggregateRoot<Guid>
    {
        [Required(ErrorMessage = "必须关联到一个项目。")]
        [Description("项目ID")]
        public Guid ProjectId { get; set; } // 外键

        [Description("关联的项目")]
        public virtual Project? Project { get; set; }// 导航属性

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

        /// <summary>
        /// 与该招采文件关联的所有决策文件 (多对多关系)。
        /// </summary>
        public virtual ICollection<DecisionDocument> RelatedDecisions { get; set; } = new List<DecisionDocument>();

        /// <summary>
        /// 从该招采文件产生的所有合同 (一对多关系)。
        /// </summary>
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

        protected Procurement() { }

        public Procurement(Guid id, Guid projectId, string code) : base(id)
        {
            ProjectId = projectId;
            DocumentCode = code;
        }
    }
}
