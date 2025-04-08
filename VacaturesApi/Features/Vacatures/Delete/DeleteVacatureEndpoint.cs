using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using VacaturesApi.Common.Dispatcher;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.Delete;

[Route("api/vacatures")]
public class DeleteVacatureEndpoint : ControllerBase
{
    private readonly Dispatcher _dispatcher;

    public DeleteVacatureEndpoint(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpDelete("{vacatureId:guid}")]
    [Authorize(Roles = "Contributor")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteVacature(
        Guid vacatureId,
        CancellationToken cancellationToken)
    {
        Log.Information("Requested to delete vacature with id: {vacatureId}", vacatureId);
        var command = new DeleteVacatureCommand(vacatureId);
        await _dispatcher.DispatchAsync<DeleteVacatureCommand, EmptyResponse>(command, cancellationToken);
        return NoContent();
    }
}