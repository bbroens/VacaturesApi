using Microsoft.EntityFrameworkCore;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;
using VacaturesApi.Persistence.Data;

namespace VacaturesApi.Features.Vacatures;

/// <summary>
/// This repository exposes methods to asynchronously get, create, update and delete Vacatures.
/// </summary>

public class VacatureRepository : IVacatureRepository
{
    private readonly VacatureDbContext _context;

    public VacatureRepository(VacatureDbContext context)
    {
        _context = context;
    }

    public async Task<Vacature?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Vacatures
            .FirstOrDefaultAsync(v => v.VacatureId == id, cancellationToken);
    }

    public async Task<List<Vacature>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.Vacatures
            .AsNoTracking()
            .OrderByDescending(v => v.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Vacature> AddAsync(Vacature vacature, CancellationToken cancellationToken)
    {
        await _context.Vacatures.AddAsync(vacature, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return vacature;
    }

    public async Task UpdateAsync(Vacature vacature, CancellationToken cancellationToken)
    {
        vacature.UpdatedAt = DateTime.UtcNow;
        _context.Vacatures.Update(vacature);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var vacature = await GetByIdAsync(id, cancellationToken);
        
        if (vacature == null)
            throw new InvalidOperationException($"Vacature with id {id} not found.");

        _context.Vacatures.Remove(vacature);
        await _context.SaveChangesAsync(cancellationToken);
    }
}