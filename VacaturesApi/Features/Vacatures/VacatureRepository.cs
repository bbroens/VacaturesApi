using Microsoft.EntityFrameworkCore;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;
using VacaturesApi.Persistence.Data;

namespace VacaturesApi.Features.Vacatures;

/// <summary>
/// This repository exposes methods to asynchronously get, create, update and delete Vacatures.
/// Error handling is done globally with the exception handling middleware.
/// Validation is done with FluentValidation on the command/query per action.
/// </summary>

public class VacatureRepository : IVacatureRepository
{
    private readonly VacatureDbContext _context;

    public VacatureRepository(VacatureDbContext context)
    {
        _context = context;
    }

    public async Task<Vacature?> GetByIdAsync(Guid vacatureId, CancellationToken cancellationToken)
    {
        var vacature = await _context.Vacatures
            .FirstOrDefaultAsync(v => v.VacatureId == vacatureId, cancellationToken);

        if (vacature == null)
            throw new NotFoundException(nameof(Vacature), vacatureId);

        return vacature;
    }

    public async Task<List<Vacature>> ListAsync(int page, int pageSize, CancellationToken cancellationToken)
    {
        return await _context.Vacatures
            .AsNoTracking()
            .OrderByDescending(v => v.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Vacatures.CountAsync(cancellationToken);
    }

    public async Task<Vacature> AddAsync(Vacature vacature, CancellationToken cancellationToken)
    {
        await _context.Vacatures.AddAsync(vacature, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return vacature;
    }

    public async Task UpdateAsync(Vacature vacature, CancellationToken cancellationToken)
    {
        // First, check if the vacature exists
        var existingVacature = await GetByIdAsync(vacature.VacatureId, cancellationToken);
        
        // Check if the vacature was updated in the meantime
        if (existingVacature!.UpdatedAt != vacature.UpdatedAt) 
            throw new ConcurrentUpdateException(nameof(Vacature), vacature.VacatureId);

        existingVacature!.UpdatedAt = DateTime.UtcNow;
        _context.Vacatures.Update(existingVacature);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid vacatureId, CancellationToken cancellationToken)
    {
        // First, check if the vacature exists
        var vacature = await GetByIdAsync(vacatureId, cancellationToken);
        
        _context.Vacatures.Remove(vacature!);
        await _context.SaveChangesAsync(cancellationToken);
    }
}