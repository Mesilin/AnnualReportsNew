using Microsoft.EntityFrameworkCore;
using YasenReports.Api.Models;

namespace YasenReports.Api.Data;

public class YasenDbContext : DbContext
{
    public YasenDbContext(DbContextOptions<YasenDbContext> options) : base(options) { }

    public DbSet<ReportFormConfig> FormConfigs => Set<ReportFormConfig>();
    public DbSet<ValidationRuleConfig> ValidationRules => Set<ValidationRuleConfig>();
    public DbSet<ReportInstance> Reports => Set<ReportInstance>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<FederalDistrict> FederalDistricts => Set<FederalDistrict>();
    public DbSet<Airbase> Airbases => Set<Airbase>();
    public DbSet<SourceDataSnapshot> SourceSnapshots => Set<SourceDataSnapshot>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // Конфигурация формы: одна активная версия на (Code, Year)
        mb.Entity<ReportFormConfig>()
            .HasIndex(x => new { x.Code, x.Year }).IsUnique();

        mb.Entity<ReportFormConfig>()
            .Property(x => x.Config)
            .HasColumnType("jsonb");

        mb.Entity<ValidationRuleConfig>()
            .HasIndex(x => new { x.Year, x.FormCode });

        // Экземпляр отчёта: один снимок на форму/год/субъект/строку
        mb.Entity<ReportInstance>()
            .HasIndex(x => new { x.FormCode, x.Year, x.SubjectId, x.RowKey }).IsUnique();

        mb.Entity<ReportInstance>()
            .Property(x => x.Data)
            .HasColumnType("jsonb");

        mb.Entity<ReportInstance>()
            .Property(x => x.ValidationJson)
            .HasColumnType("jsonb");

        mb.Entity<ReportInstance>()
            .HasOne(x => x.FormConfig)
            .WithMany()
            .HasForeignKey(x => x.FormConfigId)
            .OnDelete(DeleteBehavior.Restrict);

        mb.Entity<Subject>().HasIndex(x => x.Code).IsUnique();
        mb.Entity<Subject>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

        mb.Entity<FederalDistrict>().HasIndex(x => x.Code).IsUnique();
        mb.Entity<FederalDistrict>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

        mb.Entity<Airbase>().Property(x => x.Id).HasDefaultValueSql("gen_random_uuid()");

        mb.Entity<SourceDataSnapshot>()
            .HasIndex(x => new { x.Year, x.SubjectId, x.ColumnKey }).IsUnique();

        mb.HasPostgresExtension("pgcrypto");
    }
}
