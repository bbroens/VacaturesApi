using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VacaturesApi.Features.Vacatures.GetById;

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
        Guid VacatureId, 
        CancellationToken cancellationToken)
    {
        var query = new GetVacatureByIdQuery(VacatureId);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}