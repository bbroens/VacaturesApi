using Serilog;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Persistence.Data;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Common.Validation;
using VacaturesApi.Features.Vacatures;
using VacaturesApi.Persistence.Seeding;

// The initial bootstrap logger is able to log errors during start-up.
// On successful startup it is replaced by the logger configured in AddSerilog().
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting vacatures api...");
    
    // Add services to the build container.
    // ######################################
    
    var builder = WebApplication.CreateBuilder(args);

    // Register and config serilog
    builder.Services.AddSerilog((services, config) => config
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/logs-vacaturesapi-.txt", rollingInterval: RollingInterval.Day));    

    // Register global exception handler
    builder.Services.AddScoped<GlobalExceptionHandler>();
    
    // Add services for controllers
    builder.Services.AddControllers();
    
    // Add Swagger generator
    builder.Services.AddSwaggerGen();

    // Configure DbContext
    builder.Services.AddDbContext<VacatureDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("VacatureDbConnection")));

    // Add FluentValidation
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    
    // Register Automapper
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    
    // Register MediatR and discover and register all request handlers in the assembly.
    // Parameters of those handlers will be resolved by type using services from this DI container.
    // This allows MediatR to instance handlers with their dependencies at runtime.
    builder.Services.AddMediatR(cfg => 
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    // Register repositories
    builder.Services.AddScoped<IVacatureRepository, VacatureRepository>();
    
    // Configure the HTTP request pipeline.
    // #####################################
    
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        // Seed development Db once if empty
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<VacatureDbContext>();
        DataSeeder.Seed(dbContext);
    }
    
    // Add global exception handling middleware
    app.UseMiddleware<GlobalExceptionHandler>();
    
    if (app.Environment.IsProduction())
        app.UseHsts();
    
    app.UseAuthorization();

    app.MapControllers();
    
    app.UseSwagger();
    
    app.UseSwaggerUI();

    app.Run();

    // On application exit
    Log.Information("Web application stopped cleanly");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "The application failed to start correctly.");
    return 1;
}
finally
{
    // Write Serilog events to sinks before exit
    Log.CloseAndFlush();
}