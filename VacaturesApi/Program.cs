using Serilog;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Persistence.Data;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Features.Vacatures;
using VacaturesApi.Persistence.Seeding;

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
        .WriteTo.File("Logs/logs-vacaturesapi-.txt", rollingInterval: RollingInterval.Day));    

    builder.Services.AddControllers();

    // Configure Entity Framework with SQL Server
    builder.Services.AddDbContext<VacatureDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("VacatureDbConnection")));

    // Register automapper
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    
    // Register MediatR
    builder.Services.AddMediatR(cfg => 
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    // Register repositories
    builder.Services.AddScoped<IVacatureRepository, VacatureRepository>();

    // ##################################################
    // Configure the HTTP request pipeline.
    // ##################################################

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        // Seed dev database if it's empty
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<VacatureDbContext>();
        DataSeeder.Seed(dbContext);
    }
    
    // Add global exception handling middleware
    app.UseMiddleware<GlobalExceptionHandler>();

    // Configure the HTTP request pipeline
    if (app.Environment.IsProduction())
        app.UseHsts();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

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
