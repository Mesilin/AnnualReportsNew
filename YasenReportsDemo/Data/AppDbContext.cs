using Microsoft.EntityFrameworkCore;
using YasenReportsDemo.Models;

namespace YasenReportsDemo.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Region> Regions => Set<Region>();
    public DbSet<AirDivision> AirDivisions => Set<AirDivision>();
    public DbSet<Forestry> Forestries => Set<Forestry>();
    public DbSet<Fire> Fires => Set<Fire>();
    public DbSet<FlightHour> FlightHours => Set<FlightHour>();
    public DbSet<FormConfigVersion> FormConfigs => Set<FormConfigVersion>();
    public DbSet<ValidationRule> ValidationRules => Set<ValidationRule>();
    public DbSet<ReportInstance> ReportInstances => Set<ReportInstance>();
    public DbSet<ReportRow> ReportRows => Set<ReportRow>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<AirDivision>().HasOne(a => a.Region).WithMany().HasForeignKey(a => a.RegionId);
        mb.Entity<Forestry>().HasOne(f => f.Region).WithMany().HasForeignKey(f => f.RegionId);
        mb.Entity<Fire>().HasOne(f => f.Forestry).WithMany().HasForeignKey(f => f.ForestryId);
        mb.Entity<FlightHour>().HasOne(f => f.AirDivision).WithMany().HasForeignKey(f => f.AirDivisionId);

        mb.Entity<FormConfigVersion>().HasIndex(c => new { c.FormKey, c.Version }).IsUnique();

        mb.Entity<ReportInstance>()
            .HasIndex(r => new { r.FormKey, r.Year, r.RegionId }).IsUnique();
        mb.Entity<ReportInstance>().HasOne(r => r.Region).WithMany().HasForeignKey(r => r.RegionId);
        mb.Entity<ReportInstance>().HasOne(r => r.ConfigVersion).WithMany().HasForeignKey(r => r.ConfigVersionId);

        mb.Entity<ReportRow>().HasOne(x => x.ReportInstance).WithMany(r => r.Rows).HasForeignKey(x => x.ReportInstanceId);
        mb.Entity<ReportRow>().HasIndex(x => new { x.ReportInstanceId, x.RowKey }).IsUnique();

        // Конфиги и справочные данные храним «как есть» — без каскадных удалений справочников
        mb.Entity<Fire>().HasOne(f => f.Forestry).WithMany().HasForeignKey(f => f.ForestryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
