using Blazorise;
using Microsoft.CodeAnalysis;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Deliverables;
using ML.PCM.Entities.Payments;
using ML.PCM.Entities.Procurements;
using ML.PCM.Entities.Projects;
using ML.PCM.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Project = ML.PCM.Entities.Projects.Project;

namespace ML.PCM.Entities.Contracts
{
    /// <summary>
    /// 合同表
    /// </summary>
    public class Contract : AuditedAggregateRoot<Guid>
    {
        [Required(ErrorMessage = "必须关联到一个项目。")]
        [Description("关联的项目ID")]
        public Guid ProjectId { get; set; } // 外键

        [Description("关联的项目")]
        public virtual Project? Project { get; set; }// 导航属性

        [Description("关联的招采文件ID")]
        public Guid? ProcurementId { get; set; } // 外键 (可空)

        [Description("关联的招采文件")]
        public virtual Procurement? Procurement { get; set; } // 导航属性

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

        //[Description("累计已付额")]
        //public decimal TotalPaidAmount { get; set; } // 由 AutoMapper 实时计算

        [Description("乙方单位")]
        public string? PartyB { get; set; } // 乙方

        [Description("乙方联系人")]
        public string? ContactPerson { get; set; }

        [Description("是否虚拟合同")]
        public bool IsVirtual { get; set; }

        [Description("备注")]
        public string? Remarks { get; set; }

        [Description("关联的资金支付")]
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
        [Description("关联的决策文件")]
        public virtual ICollection<DecisionDocument> RelatedDecisions { get; set; } = new List<DecisionDocument>();
        [Description("关联的成果文件")]
        public virtual ICollection<Deliverable> Deliverables { get; set; } = new List<Deliverable>();


        public Contract() { }

        public Contract(Guid id, Guid projectId, string code, string name) : base(id)
        {
            ProjectId = projectId;
            DocumentCode = code;
            Name = name;
        }
    }
}
