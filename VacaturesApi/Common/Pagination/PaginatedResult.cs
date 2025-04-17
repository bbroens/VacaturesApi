namespace VacaturesApi.Common.Pagination;

/// <summary>
/// Describes the result of a paginated query.
/// </summary>

public class PaginatedResult<T>
{
    public List<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}