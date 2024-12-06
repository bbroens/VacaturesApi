using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Endpoint for fetching a vacature by its id.
/// </summary>

[Route("api/vacatures")]
public class GetVacatureByIdEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public GetVacatureByIdEndpoint(IMediator mediator, ILogger<GetVacatureByIdEndpoint> logger)
    {
        _mediator = mediator;
    }

    [HttpGet("{VacatureId:guid}")]
    [ProducesResponseType(typeof(VacatureDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VacatureDto>> GetVacatureById(
        Guid vacatureId, 
        CancellationToken cancellationToken)
    {
        var query = new GetVacatureByIdQuery(vacatureId);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}