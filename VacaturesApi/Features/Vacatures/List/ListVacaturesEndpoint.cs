using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using VacaturesApi.Common.Pagination;

namespace VacaturesApi.Features.Vacatures.List;

/// <summary>
/// Endpoint for retrieving a paginated & cached list of all vacatures ordered by creation date.
/// </summary>

[Route("api/vacatures")]
public class ListVacaturesEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public ListVacaturesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ResponseCache(Duration = 30)] // cache response for 30 seconds
    [EnableRateLimiting("ExpensiveEndpointsPolicy")] // apply special rate limiting
    [ProducesResponseType(typeof(PaginatedResult<VacatureDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedResult<VacatureDto>>> ListVacatures(
        CancellationToken cancellationToken,
        [FromQuery] int page = 1, // default current page
        [FromQuery] int pageSize = 10) // default items per page
    {
        var query = new ListVacaturesQuery(page, pageSize);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}