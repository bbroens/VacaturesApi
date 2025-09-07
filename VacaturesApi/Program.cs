using Serilog;
using Mapster;
using FluentValidation;
using System.Reflection;
using VacaturesApi.Common.Dispatcher;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Persistence.Data;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Common.RequestBehaviors;
using VacaturesApi.Features.Authentication;
using VacaturesApi.Features.Vacatures;
using VacaturesApi.Persistence.Seeding;
using VacaturesApi.ServiceExtensions;

// Bootstrap logger to log errors during startup. Replaced by ConfigureSerilog()
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Vacatures API...");
    
    // ### Add services to the build container.
    
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

    // Register ServiceExtensions
    builder.Services.ConfigureSerilog(builder.Configuration);
    builder.Services.ConfigureCors();
    builder.Services.ConfigureIISIntegration();
    builder.Services.ConfigureDbContext(builder.Configuration);
    builder.Services.ConfigureRateLimiter();
    builder.Services.ConfigureResponseCaching();
    builder.Services.ConfigureIdentity();
    builder.Services.ConfigureJwtAuthentication(builder.Configuration);
    
    builder.Services.AddControllers();
    
    builder.Services.AddSwaggerGen();

    // Add FluentValidation
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    builder.Services.AddTransient(typeof(IRequestBehavior<,>), typeof(ValidationBehavior<,>));
    
    builder.Services.AddMapster();
    TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    
    // Register CQRS Dispatcher and register all request handlers in the assembly
    builder.Services.AddScoped<Dispatcher>();
    builder.Services.AddHandlers(Assembly.GetExecutingAssembly());

    // Register repositories
    builder.Services.AddScoped<IVacatureRepository, VacatureRepository>();
    builder.Services.AddScoped<AuthRepository>();
    
    // ### Configure the HTTP request pipeline
    
    var app = builder.Build();
    var enableHttpsRedirection = builder.Configuration.GetValue<bool?>("EnableHttpsRedirection") ?? true;

    if (app.Environment.IsDevelopment())
    {
        // Seed development DB once if empty
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<VacatureDbContext>();
        DataSeeder.Seed(dbContext);
    }
    
    app.UseExceptionHandler(b => { });
    
    app.UseSerilogRequestLogging();

    if (app.Environment.IsProduction())
    {
        app.UseHsts();
    }

    if (enableHttpsRedirection)
    {
        app.UseHttpsRedirection();
    }

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
    
    Log.Information("Vacatures API stopped cleanly.");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Vacatures API failed to start correctly.");
    return 1;
}
finally
{
    // Write Serilog events to sinks
    Log.CloseAndFlush();
}