using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VacaturesApi.Domain;

namespace VacaturesApi.Persistence.Data;

/// <summary>
/// This class implements an EF DbContext used by the app to interact with the database.
/// </summary>

public class VacatureDbContext : DbContext
{
    public VacatureDbContext(DbContextOptions<VacatureDbContext> options) : base(options) {}
    
    // This DbSet represents the Vacature table
    public DbSet<Vacature> Vacatures { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}