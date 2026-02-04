using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Entities.Contracts;
using ML.PCM.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace ML.PCM.Entities.Payments
{
    /// <summary>
    /// 资金支付表
    /// </summary>
    public class Payment : AuditedAggregateRoot<Guid>
    {
        [Required(ErrorMessage = "必须关联到一个合同。")]
        [Description("合同ID")]
        public Guid ContractId { get; set; } // 外键

        [Description("关联的合同")]
        public virtual Contract? Contract { get; set; } // 导航属性

        [Description("支付编号")]
        public string DocumentCode { get; set; }

        [Description("费用类别")]
        public PaymentCategory? Category { get; set; }

        [Description("支付批次(第几次)")]
        public int? PaymentNumber { get; set; }

        [Description("摘要")]
        public string? Summary { get; set; }

        [Description("审批文号")]
        public string? ReferenceNumber { get; set; }

        [Description("审批日期")]
        public DateTime? ApprovalDate { get; set; }

        //[Required(ErrorMessage = "审签应付额是必需的。")]
        //[Description("审签应付额")]
        //public decimal AuditedPayableAmount { get; set; }

        [Description("本次支付金额")]
        public decimal AmountPaid { get; set; }

        [Description("备注")]
        public string? Remarks { get; set; }

        protected Payment() { }

        public Payment(Guid id, Guid contractId, string code) : base(id)
        {
            ContractId = contractId;
            DocumentCode = code;
        }
    }
}
