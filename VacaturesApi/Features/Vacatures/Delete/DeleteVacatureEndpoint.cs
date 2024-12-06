using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Endpoint for deleting a specific vacature
/// </summary>

[Route("api/vacatures")]
public class DeleteVacatureEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteVacatureEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpDelete("{vacatureId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteVacature(
        Guid vacatureId, 
        CancellationToken cancellationToken)
    {
        Log.Information("Requested to delete vacature with id: {vacatureId}", vacatureId);
        var command = new DeleteVacatureCommand(vacatureId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}