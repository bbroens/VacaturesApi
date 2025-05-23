﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VacaturesApi.Domain;
using System.Reflection;
using VacaturesApi.Features.Authentication;

namespace VacaturesApi.Persistence.Data;

/// <summary>
/// DbContext for vacature entities.
/// </summary>

public class VacatureDbContext : IdentityDbContext<ApplicationUser>
{
    public VacatureDbContext(DbContextOptions<VacatureDbContext> options) : base(options) { }

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