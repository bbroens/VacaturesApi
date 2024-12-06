﻿namespace VacaturesApi.Common.Pagination;

/// <summary>
/// Describes the result of a paginated query.
/// </summary>

public class PaginatedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}