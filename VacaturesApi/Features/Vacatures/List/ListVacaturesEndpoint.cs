using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacaturesApi.Features.Vacatures.List;

/// <summary>
/// Endpoint for retrieving a list of all vacatures ordered by creation date
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
    [ProducesResponseType(typeof(List<VacatureDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<VacatureDto>>> ListVacatures(CancellationToken cancellationToken)
    {
        var query = new ListVacaturesQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}