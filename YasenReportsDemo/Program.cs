using Microsoft.EntityFrameworkCore;
using YasenReportsDemo.Data;
using YasenReportsDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<FormConfigService>();
builder.Services.AddScoped<SqlComputedService>();
builder.Services.AddScoped<ValidationService>();
builder.Services.AddScoped<ReportService>();

var app = builder.Build();

// Автомиграция при старте (демо). В production — отдельный шаг деплоя.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
