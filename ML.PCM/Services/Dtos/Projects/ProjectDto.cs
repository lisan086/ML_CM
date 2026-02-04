using System;
using System.ComponentModel;
using ML.PCM.Enums;
using Volo.Abp.Application.Dtos;

namespace ML.PCM.Services.Dtos.Projects
{
    [Description("项目")]
    public class ProjectDto: AuditedEntityDto<Guid>
    {
        [Description("项目编号")]
        public string ProjectCode { get; set; } = string.Empty;

        [Description("项目名称")]
        public string Name { get; set; } = string.Empty;

        [Description("所属类别")]
        public ProjectCategory? Category { get; set; }

        [Description("主要建设内容")]
        public string? MainContent { get; set; }

        [Description("项目总投资 (万元)")]
        public decimal? TotalInvestment { get; set; }

        [Description("项目负责人")]
        public string? Manager { get; set; }

        [Description("项目经办人")]
        public string? Operator { get; set; }

        [Description("备注")]
        public string? Remarks { get; set; }

        // 审计字段，用于在UI上显示
        [Description("创建者")]
        public string? CreatedBy { get; set; }
        [Description("创建时间")]
        public DateTime? CreatedAt { get; set; }
        [Description("最后修改者")]
        public string? LastModifiedBy { get; set; }
        [Description("最后修改时间")]
        public DateTime? LastModifiedAt { get; set; }

       
    }
}
