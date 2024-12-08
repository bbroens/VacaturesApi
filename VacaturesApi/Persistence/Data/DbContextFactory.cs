using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VacaturesApi.Persistence.Data;

/// <summary>
/// Factory class for creating instances of VacatureDbContext at design time.
/// This can provide a direct DbContext instance when running EF migrations.
/// </summary>

public class DbContextFactory : IDesignTimeDbContextFactory<VacatureDbContext>
{
    public VacatureDbContext CreateDbContext(string[] args)
    {
        // Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        // Get connection string
        var connectionString = configuration.GetConnectionString("VacatureDbConnection");

        // Create DbContextOptionsBuilder
        var optionsBuilder = new DbContextOptionsBuilder<VacatureDbContext>();
        optionsBuilder.UseSqlServer(connectionString, 
            b => b.MigrationsAssembly(typeof(VacatureDbContext).Assembly.FullName));

        // Return context
        return new VacatureDbContext(optionsBuilder.Options);
    }
}