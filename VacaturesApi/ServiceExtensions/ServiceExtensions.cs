using Serilog;
using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using VacaturesApi.Persistence.Data;

namespace VacaturesApi.ServiceExtensions;

/// <summary>
/// Contains extension methods for configuring services in the Program.cs.
/// By defining the configuration here we can keep the Program.cs clean and focused.
/// </summary>

public static class ServiceExtensions
{
    // Serilog configuration
    public static void ConfigureSerilog(this IServiceCollection services, IConfiguration config)
    {
        services.AddSerilog((services, options) => options
            .ReadFrom.Configuration(config)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("Logs/logs-vacaturesapi-.txt", rollingInterval: RollingInterval.Day));
    }
    
    // Global CORS configuration
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.WithOrigins("https://localhost:5001")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
        });
    }
    
    // Configure IIS integration for self-hosted
    public static void ConfigureIISIntegration(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options => { });
    }
    
    // Configure DbContext
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VacatureDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("VacatureDbConnection")));
    }
    
    // Configure global rate limiter for the API
    public static void ConfigureRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            // Limit requests per minute for all endpoints
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown",
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 100,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }));
            
            // Limit requests per minute specifically for expensive endpoints
            options.AddPolicy("ExpensiveEndpointsPolicy", context =>
                RateLimitPartition.GetFixedWindowLimiter("ExpensiveEndpointsLimiter",
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 50,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        
            options.RejectionStatusCode = 429;

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.", token);
            };
        });
    }
    
    // Configure response caching
    public static void ConfigureResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }
}