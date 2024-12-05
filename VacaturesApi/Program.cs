using Microsoft.EntityFrameworkCore;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Features.Vacatures;
using VacaturesApi.Persistence.Data;
using Serilog;

// Recommended by Serilog:
// The initial "bootstrap" logger is able to log errors during start-up. It's replaced by the logger
// configured in `AddSerilog()` below, once config and dependency-injection have been set up successfully.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");

    // ##################################################
    // Add services to the build container.
    // ##################################################
    
    var builder = WebApplication.CreateBuilder(args);
    
    // Register and config serilog to use logging
    builder.Services.AddSerilog((services, config) => config
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/logs-.txt", rollingInterval: RollingInterval.Day));    

    builder.Services.AddControllers();
    
    // Configure DbContext
    builder.Services.AddDbContext<VacatureDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(VacatureDbContext).Assembly.FullName)
        )
    );
    
    // Register repositories
    builder.Services.AddScoped<IVacatureRepository, VacatureRepository>();

    // ##################################################
    // Configure the HTTP request pipeline.
    // ##################################################

    var app = builder.Build();
    
    // Use the strict-transport-security header in production
    if (app.Environment.IsProduction())
        app.UseHsts();

    app.UseHttpsRedirection();
    
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    
    // When this point is reached, the application has stopped without errors
    Log.Information("Web application stopped cleanly");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during logger bootstrapping");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}