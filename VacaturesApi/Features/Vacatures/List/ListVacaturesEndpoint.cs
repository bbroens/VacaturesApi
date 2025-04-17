using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using VacaturesApi.Common.Dispatcher;
using VacaturesApi.Common.Pagination;

namespace VacaturesApi.Features.Vacatures.List;

/// <summary>
/// Endpoint for retrieving a paginated, cached list of all vacatures.
/// </summary>

[Route("api/vacatures")]
public class ListVacaturesEndpoint : ControllerBase
{
    private readonly Dispatcher _dispatcher;

    public ListVacaturesEndpoint(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    [ResponseCache(Duration = 30)] // Cache response for 30 seconds
    [EnableRateLimiting("ExpensiveEndpointsPolicy")] // Apply expensive rate limiting
    [ProducesResponseType(typeof(PaginatedResult<VacatureDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<VacatureDto>>> ListVacatures(
        CancellationToken cancellationToken,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new ListVacaturesQuery(page, pageSize);
        var result = await _dispatcher.DispatchAsync<ListVacaturesQuery, PaginatedResult<VacatureDto>>(query, cancellationToken);
        return Ok(result);
    }
}