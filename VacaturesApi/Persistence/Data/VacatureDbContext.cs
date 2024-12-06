using Microsoft.EntityFrameworkCore;
using VacaturesApi.Domain;
using System.Reflection;

namespace VacaturesApi.Persistence.Data;

/// <summary>
/// This class implements an EF DbContext used by the app to interact with the database.
/// </summary>

public class VacatureDbContext : DbContext
{
    public VacatureDbContext(DbContextOptions<VacatureDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Vacature> Vacatures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply all entity type configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Automatically set CreatedAt for new entities
        foreach (var entry in ChangeTracker.Entries<Vacature>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}