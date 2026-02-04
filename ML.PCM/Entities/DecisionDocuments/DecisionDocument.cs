using ML.PCM.Entities.Procurements;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Entities.Projects;
using ML.PCM.Enums;
using Volo.Abp.Domain.Entities.Auditing;
using ML.PCM.Entities.Contracts;

namespace ML.PCM.Entities.DecisionDocuments
{
    /// <summary>
    /// 决策文件表
    /// </summary>
    public class DecisionDocument : AuditedAggregateRoot<Guid>
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
        
        [Required(ErrorMessage = "文件名称是必需的。")]
        [StringLength(200)]
        [Description("文件名称")]
        public string Name { get; set; } = string.Empty;

        [Description("文件分类")]
        public DecisionCategory? Category { get; set; }

        [StringLength(100)]
        [Description("文号")]
        public string? ReferenceNumber { get; set; }

        [Description("成文日期")]
        public DateTime? DocumentDate { get; set; }

        [Description("备注")]
        public string? Remarks { get; set; }

        /// <summary>
        /// 关联到此决策文件的所有合同。
        /// </summary>
        public virtual ICollection<Contract> RelatedContracts { get; set; } = new List<Contract>();

        /// <summary>
        /// 关联到此决策文件的所有招采信息。
        /// </summary>
        public virtual ICollection<Procurement> RelatedProcurements { get; set; } = new List<Procurement>();

        public DecisionDocument() { } // EF Core 需要

        // 强制构造函数
        public DecisionDocument(Guid id, Guid projectId, string code, string name) : base(id)
        {
            this.ProjectId = projectId;
            DocumentCode = code;
            Name = name;
        }
    }
}
