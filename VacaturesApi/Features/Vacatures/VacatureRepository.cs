using Microsoft.EntityFrameworkCore;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;
using VacaturesApi.Persistence.Data;

namespace VacaturesApi.Features.Vacatures;

/// <summary>
/// This repository exposes methods to asynchronously get, create, update and delete Vacatures.
/// Error handling is done globally with the exception handling middleware.
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
        var vacature = await _context.Vacatures
            .FirstOrDefaultAsync(v => v.VacatureId == id, cancellationToken);

        if (vacature == null)
            throw new NotFoundException(nameof(Vacature), id);

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
        try
        {
            await _context.Vacatures.AddAsync(vacature, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return vacature;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error occurred while adding vacature.", ex);
        }
    }

    public async Task UpdateAsync(Vacature vacature, CancellationToken cancellationToken)
    {
        // First, verify the vacature exists
        var existingVacature = await GetByIdAsync(vacature.VacatureId, cancellationToken);

        try
        {
            existingVacature.UpdatedAt = DateTime.UtcNow;
            _context.Vacatures.Update(existingVacature);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error occurred while updating vacature.", ex);
        }
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        // Check if the vacature exists or throw a NotFoundException
        var vacature = await GetByIdAsync(id, cancellationToken);

        try
        {
            _context.Vacatures.Remove(vacature);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error occurred while deleting vacature.", ex);
        }
    }
}