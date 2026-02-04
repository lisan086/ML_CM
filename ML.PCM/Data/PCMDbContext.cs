using Microsoft.EntityFrameworkCore;
using ML.PCM.Entities.Books;
using ML.PCM.Entities.Contracts;
using ML.PCM.Entities.DecisionDocuments;
using ML.PCM.Entities.Deliverables;
using ML.PCM.Entities.Payments;
using ML.PCM.Entities.Permits;
using ML.PCM.Entities.Procurements;
using ML.PCM.Entities.Projects; // 引用实体
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace ML.PCM.Data;

public class PCMDbContext : AbpDbContext<PCMDbContext>
{
    public DbSet<Book> Books { get; set; }

    // 1. 添加 DbSet
    public DbSet<Project> Projects { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Procurement> Procurements { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<DecisionDocument> DecisionDocuments { get; set; }
    public DbSet<Permit> Permits { get; set; }
    public DbSet<Deliverable> Deliverables { get; set; }

    public const string DbTablePrefix = "App";
    public const string DbSchema = null;

    public PCMDbContext(DbContextOptions<PCMDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigurePermissionManagement();
        builder.ConfigureBlobStoring();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        
        builder.Entity<Book>(b =>
        {
            b.ToTable(DbTablePrefix + "Books",
                DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
        });

        /* Configure your own entities here */
        // 2. 配置映射
        // 2.1 项目 (Projects)
        builder.Entity<Project>(b =>
        {
            b.ToTable(DbTablePrefix + "Projects", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.ProjectCode).HasMaxLength(50); // 建议给编号加长度限制
            b.HasIndex(x => x.ProjectCode);
        });

        // 2.2 合同 (Contracts)
        builder.Entity<Contract>(b =>
        {
            b.ToTable(DbTablePrefix + "Contracts", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.DocumentCode).HasMaxLength(100);
            b.Property(x => x.ContractAmount).HasPrecision(18, 2); // 金额精度
            // 关系：合同属于项目
            b.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);
        });

        // 2.3 招采信息 (Procurements)
        builder.Entity<Procurement>(b =>
        {
            b.ToTable(DbTablePrefix + "Procurements", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.DocumentCode).HasMaxLength(100);
            b.Property(x => x.ControlPrice).HasPrecision(18, 2);
            b.Property(x => x.WinningBidAmount).HasPrecision(18, 2);

            // 关系：招采属于项目
            b.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);

            // ✅【修复】配置多对多关系：招采 <-> 决策文件
            // 显式指定中间表配置，将 OnDelete 设置为 Restrict (禁止级联删除)
            b.HasMany(p => p.RelatedDecisions)
                .WithMany(d => d.RelatedProcurements)
                .UsingEntity<Dictionary<string, object>>(
                    "DecisionDocumentProcurement", // 中间表名称
                    right => right.HasOne<DecisionDocument>()
                        .WithMany()
                        .HasForeignKey("RelatedDecisionsId")
                        .OnDelete(DeleteBehavior.Restrict), // 禁用级联删除
                    left => left.HasOne<Procurement>()
                        .WithMany()
                        .HasForeignKey("RelatedProcurementsId")
                        .OnDelete(DeleteBehavior.Restrict)    // 禁用级联删除
                );
        });

        // 2.4 资金支付 (Payments)
        builder.Entity<Payment>(b =>
        {
            b.ToTable(DbTablePrefix + "Payments", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.DocumentCode).HasMaxLength(100);
            b.Property(x => x.AmountPaid).HasPrecision(18, 2); // 金额精度

            // 关系：支付属于合同
            b.HasOne(x => x.Contract).WithMany().HasForeignKey(x => x.ContractId);
        });

        // 2.5 决策文件 (DecisionDocuments)
        builder.Entity<DecisionDocument>(b =>
        {
            b.ToTable(DbTablePrefix + "DecisionDocuments", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.DocumentCode).HasMaxLength(100);

            // 关系：决策文件属于项目
            b.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);

            // ✅【修复】如果你在 Contract 中也关联了 DecisionDocument，可能也需要类似的配置
            // 这里添加 Contract <-> DecisionDocument 的多对多配置 (防止同样的错误)
            b.HasMany(d => d.RelatedContracts)
                .WithMany(c => c.RelatedDecisions)
                .UsingEntity<Dictionary<string, object>>(
                    "ContractDecisionDocument",
                    right => right.HasOne<Contract>()
                        .WithMany()
                        .HasForeignKey("RelatedContractsId")
                        .OnDelete(DeleteBehavior.Restrict),
                    left => left.HasOne<DecisionDocument>()
                        .WithMany()
                        .HasForeignKey("RelatedDecisionsId")
                        .OnDelete(DeleteBehavior.Restrict)
                );
        });

        // 2.6 行政许可 (Permits)
        builder.Entity<Permit>(b =>
        {
            b.ToTable(DbTablePrefix + "Permits", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.DocumentCode).HasMaxLength(100);

            // 关系：行政许可属于项目
            b.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);
        });

        // 2.7 成果文件 (Deliverables)
        builder.Entity<Deliverable>(b =>
        {
            b.ToTable(DbTablePrefix + "Deliverables", DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(200);
            b.Property(x => x.DocumentCode).HasMaxLength(100);

            // 关系：成果文件属于合同 (通常成果是跟着合同走的，或者你可以改为跟项目 ProjectId)
            b.HasOne(x => x.Contract).WithMany().HasForeignKey(x => x.ContractId);
        });
    }
}

