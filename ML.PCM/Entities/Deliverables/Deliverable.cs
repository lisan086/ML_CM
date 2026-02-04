using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Entities.Contracts;
using ML.PCM.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace ML.PCM.Entities.Deliverables
{
    public class Deliverable : AuditedAggregateRoot<Guid>
    {
        [Required(ErrorMessage = "必须关联到一个合同。")]
        [Description("合同ID")]
        public Guid ContractId { get; set; } // 外键

        [Description("关联的合同")]
        public virtual Contract? Contract { get; set; } // 导航属性

        [Description("文件编号")]
        public string DocumentCode { get; set; }

        [Description("成果分类")]
        public DeliverableCategory? Category { get; set; }

        [Description("成果名称")]
        public string Name { get; set; } = string.Empty;

        [Description("摘要")]
        public string? Summary { get; set; }

        [Description("文号")]
        public string? ReferenceNumber { get; set; }

        [Description("审批日期")]
        public DateTime? ApprovalDate { get; set; }

        protected Deliverable() { }

        // 强制构造
        public Deliverable(Guid id, Guid contractId, string code, string name) : base(id)
        {
            ContractId = contractId;
            DocumentCode = code;
            Name = name;
        }
    }
}
