using Serilog;
using MediatR;
using FluentValidation;
using System.Reflection;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Persistence.Data;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Common.Validation;
using VacaturesApi.Features.Vacatures;
using VacaturesApi.Persistence.Seeding;
using VacaturesApi.ServiceExtensions;

// The initial bootstrap logger is able to log errors during start-up.
// On successful startup it is replaced by the logger configured by ConfigureSerilog().
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting vacatures api...");
    
    // Add services to the build container.
    // ######################################
    
    var builder = WebApplication.CreateBuilder(args);
    
    // Register our custom global IExceptionHandler
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    // Register our (/ServiceExtensions) Serilog Config
    builder.Services.ConfigureSerilog(builder.Configuration);

    // Register our (/ServiceExtensions) CORS Config
    builder.Services.ConfigureCors();

    // Register our (/ServiceExtensions) IIS Integration
    builder.Services.ConfigureIISIntegration();
    
    // Register our (/ServiceExtensions) DbContext used by the repositories
    builder.Services.ConfigureDbContext(builder.Configuration);
    
    // Register our (/ServiceExtensions) Rate Limiter
    builder.Services.ConfigureRateLimiter();
    
    // Add services for controllers
    builder.Services.AddControllers();
    
    // Add Swagger generator
    builder.Services.AddSwaggerGen();

    // Add FluentValidation
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    
    // Register Automapper
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    
    // Register MediatR and discover and register all request handlers in the assembly.
    // Parameters of those handlers will be resolved by type using services from this DI container.
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
    
    app.UseExceptionHandler(b => { });
    
    if (app.Environment.IsProduction())
        app.UseHsts();
    
    app.UseHttpsRedirection();
    
    app.UseCors("CorsPolicy");
    
    app.UseAuthorization();

    app.UseRateLimiter();
    
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