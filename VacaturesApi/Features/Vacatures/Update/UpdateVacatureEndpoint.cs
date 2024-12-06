using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Endpoint for updating an existing vacature
/// </summary>

[Route("api/vacatures")]
public class UpdateVacatureEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateVacatureEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("{vacatureId:guid}")]
    [ProducesResponseType(typeof(VacatureDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VacatureDto>> UpdateVacature(
        Guid vacatureId, 
        [FromBody] UpdateVacatureDto updateDto, 
        CancellationToken cancellationToken)
    {
        Log.Information("Attempting to update vacature with id: {Id}", vacatureId);
        
        // plug the vacatureId from the route into the updateDto
        updateDto.VacatureId = vacatureId;
        
        var command = new UpdateVacatureCommand(updateDto);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}