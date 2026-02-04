using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Permits;
using ML.PCM.Entities.Procurements;
using ML.PCM.Enums;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace ML.PCM.Entities.Projects
{
    /// <summary>
    /// 项目表
    /// </summary>
    public class Project : AuditedAggregateRoot<Guid>
    {
        [Required(ErrorMessage = "项目编号是必需的。")]
        [StringLength(50)]
        public string ProjectCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "项目名称是必需的。")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Description("所属类别")]
        public ProjectCategory? Category { get; set; }

        [Description("主要建设内容")]
        public string? MainContent { get; set; }

        [Description("项目总投资 (万元)")]
        public decimal? TotalInvestment { get; set; }

        [StringLength(100)]
        [Description("项目负责人")]
        public string? Manager { get; set; }

        [StringLength(100)]
        [Description("项目经办人")]
        public string? Operator { get; set; }

        [Description("备注")]
        public string? Remarks { get; set; }

        /// <summary>
        /// 与该项目关联的所有决策文件。
        /// </summary>
        public virtual ICollection<DecisionDocument> DecisionDocuments { get; set; } = new List<DecisionDocument>();

        /// <summary>
        /// 与该项目关联的所有行政许可。
        /// </summary>
        public virtual ICollection<Permit> Permits { get; set; } = new List<Permit>();

        /// <summary>
        /// 与该项目关联的所有招采信息。
        /// </summary>
        public virtual ICollection<Procurement> Procurements { get; set; } = new List<Procurement>();

        /// <summary>
        /// 与该项目关联的所有合同。
        /// </summary>
        public virtual ICollection<ML.PCM.Entities.Contracts.Contract> Contracts { get; set; } = new List<ML.PCM.Entities.Contracts.Contract>();

        // 1. 必须保留这个 protected 空构造函数给 EF Core 使用
        public Project() { }

        // 2. 添加/修改这个构造函数
        // 我们强制要求创建时必须传入 Code 和 Name
        public Project(Guid id, string projectCode, string name) : base(id)
        {
            ProjectCode = projectCode;
            Name = name;
        }
    }
}
