using VacaturesApi.Domain;

namespace VacaturesApi.Common.Interfaces;

/// <summary>
/// Interface describing repository of async methods for handling Vacatures.
/// </summary>

public interface IVacatureRepository
{
    Task<Vacature?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Vacature>> ListAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<Vacature> AddAsync(Vacature vacature, CancellationToken cancellationToken);
    Task UpdateAsync(Vacature vacature, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}