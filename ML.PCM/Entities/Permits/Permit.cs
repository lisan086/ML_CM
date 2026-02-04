using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ML.PCM.Entities.Projects;
using ML.PCM.Enums;
using Volo.Abp.Domain.Entities.Auditing;

namespace ML.PCM.Entities.Permits
{
    /// <summary>
    /// 行政许可表
    /// </summary>
    public class Permit : AuditedAggregateRoot<Guid>
    {
        [Required(ErrorMessage = "必须关联到一个项目。")]
        [Description("项目ID")]
        public Guid ProjectId { get; set; } // 外键

        [Description("关联的项目")]
        public virtual Project? Project { get; set; } // 导航属性

        [Required(ErrorMessage = "文件编号是必需的。")]
        [StringLength(100)]
        [Description("文件编号")]
        public string DocumentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "文件名称是必需的。")]
        [StringLength(200)]
        [Description("文件名称")]
        public string Name { get; set; } = string.Empty;

        [Description("许可分类")]
        public PermitCategory? Category { get; set; }

        [Description("摘要")]
        public string? Summary { get; set; }

        [StringLength(100)]
        [Description("文号")]
        public string? ReferenceNumber { get; set; }

        [Description("批复日期")]
        public DateTime? ApprovalDate { get; set; }

        [Description("备注")]
        public string? Remarks { get; set; }

        protected Permit() { }

        // 强制构造
        public Permit(Guid id, Guid projectId, string code, string name) : base(id)
        {
            ProjectId = projectId;
            DocumentCode = code;
            Name = name;
        }
    }
}
