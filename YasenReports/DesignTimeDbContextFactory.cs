using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using YasenReports.Api.Data;

namespace YasenReports.Api;

/// <summary>Фабрика контекста для dotnet-ef (миграции, update).</summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<YasenDbContext>
{
    public YasenDbContext CreateDbContext(string[] args)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("YASEN_CONNECTION")
            ?? "Host=localhost;Port=5432;Database=YasenReports;Username=postgres;Password=123456";

        var options = new DbContextOptionsBuilder<YasenDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new YasenDbContext(options);
    }
}
