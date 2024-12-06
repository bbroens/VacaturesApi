using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using VacaturesApi.Common.Exceptions;

namespace VacaturesApi.Features.Vacatures.Get;

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
        try
        {
            Log.Information("Fetching vacature with ID: {Id}", VacatureId);
            var query = new GetVacatureByIdQuery(VacatureId);
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }
        catch (NotFoundException)
        {
            Log.Warning("Vacature not found with ID: {Id}", VacatureId);
            return NotFound();
        }
    }
}