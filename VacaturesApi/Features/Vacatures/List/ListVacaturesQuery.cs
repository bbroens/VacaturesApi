using MediatR;
using VacaturesApi.Common.Pagination;

namespace VacaturesApi.Features.Vacatures.List;

/// <summary>
/// Query record describing a request to return a paginated list of vacatures.
/// Carries no data for the handler listening for it.
/// Tells Mediatr that this request must return a paginated list of VacatureDto.
/// </summary>

public record ListVacaturesQuery : IRequest<PaginatedResult<VacatureDto>>
{
    public int Page { get; }
    public int PageSize { get; }

    public ListVacaturesQuery(int page = 1, int pageSize = 10)
    {
        Page = page;
        PageSize = pageSize;
    }
}