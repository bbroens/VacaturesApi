using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using VacaturesApi.Common.Dispatcher;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Endpoint for updating an existing vacature
/// </summary>

[Route("api/vacatures")]
public class UpdateVacatureEndpoint : ControllerBase
{
    private readonly Dispatcher _dispatcher;

    public UpdateVacatureEndpoint(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPut("{vacatureId:guid}")]
    [Authorize(Roles = "Contributor")]
    [ProducesResponseType(typeof(VacatureDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<VacatureDto>> UpdateVacature(
        Guid vacatureId,
        [FromBody] UpdateVacatureDto updateDto,
        CancellationToken cancellationToken)
    {
        Log.Information("Attempting to update vacature with id: {Id}", vacatureId);

        // plug the vacatureId from the route into the updateDto
        updateDto.VacatureId = vacatureId;

        var command = new UpdateVacatureCommand(updateDto);
        var result = await _dispatcher.DispatchAsync<UpdateVacatureCommand, VacatureDto>(command, cancellationToken);
        return Ok(result);
    }
}