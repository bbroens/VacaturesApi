using System.Text;
using Serilog;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VacaturesApi.Features.Authentication;
using VacaturesApi.Persistence.Data;

namespace VacaturesApi.ServiceExtensions;

/// <summary>
/// Contains extension methods for configuring services in the Program.cs.
/// </summary>

public static class ServiceExtensions
{
    // Serilog configuration
    public static void ConfigureSerilog(this IServiceCollection services, IConfiguration config)
    {
        services.AddSerilog((serviceprovider, options) => options
            .ReadFrom.Configuration(config)
            .ReadFrom.Services(serviceprovider)
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
    
    // Configure Identity Framework for role-based auth
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<VacatureDbContext>()
            .AddDefaultTokenProviders();
    }
    
    // Configure JWT Authentication
    public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure JWT Authentication
        var jwtSettings = configuration.GetSection("Jwt");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        if (secretKey == null) return;
        var key = Encoding.ASCII.GetBytes(secretKey);
        
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }
}