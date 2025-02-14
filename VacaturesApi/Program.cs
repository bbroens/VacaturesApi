using Serilog;
using MediatR;
using FluentValidation;
using System.Reflection;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Persistence.Data;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Common.Validation;
using VacaturesApi.Features.Authentication;
using VacaturesApi.Features.Vacatures;
using VacaturesApi.Persistence.Seeding;
using VacaturesApi.ServiceExtensions;

// The initial bootstrap logger is able to log errors during start-up. It's replaced by ConfigureSerilog().
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting vacatures api...");
    
    // ### Add services to the build container.
    
    var builder = WebApplication.CreateBuilder(args);
    
    // Register our custom global IExceptionHandler
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    // Register services from the ServiceExtensions class
    builder.Services.ConfigureSerilog(builder.Configuration);
    builder.Services.ConfigureCors();
    builder.Services.ConfigureIISIntegration();
    builder.Services.ConfigureDbContext(builder.Configuration);
    builder.Services.ConfigureRateLimiter();
    builder.Services.ConfigureResponseCaching();
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJwtAuthentication(builder.Configuration);
    
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
    builder.Services.AddMediatR(cfg => 
        cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    // Register repositories
    builder.Services.AddScoped<IVacatureRepository, VacatureRepository>();
    builder.Services.AddScoped<AuthRepository>();
    
    // ### Configure the HTTP request pipeline.
    
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
    
    // Write Serilog request events instead of the built-in ones.
    app.UseSerilogRequestLogging();
    
    app.UseHttpsRedirection();
    
    app.UseCors("CorsPolicy");
    
    app.UseRouting();

    app.UseAuthentication();
    
    app.UseAuthorization();

    app.UseRateLimiter();
    
    app.MapControllers();
    
    app.UseSwagger();
    
    app.UseSwaggerUI();
    
    app.UseResponseCaching();

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