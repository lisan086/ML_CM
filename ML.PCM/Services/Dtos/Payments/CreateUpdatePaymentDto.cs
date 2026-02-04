using ML.PCM.Enums;
using ML.PCM.Services.Dtos.Contracts;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML.PCM.Services.Dtos.Payments;

public class CreateUpdatePaymentDto
{
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

    [Description("合同ID")]
    public Guid ContractId { get; set; } // 外键

    [Description("关联的合同")]
    public virtual ContractDto? Contract { get; set; } // 导航属性

}