using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using VacaturesApi.Common.Dispatcher;

namespace VacaturesApi.Features.Vacatures.Create;

/// <summary>
/// Endpoint for creating a new vacature.
/// </summary>

[Route("api/vacatures")]
public class CreateVacatureEndpoint : ControllerBase
{
    private readonly Dispatcher _dispatcher;

    public CreateVacatureEndpoint(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    [Authorize(Roles = "Contributor")]
    [ProducesResponseType(typeof(VacatureDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<VacatureDto>> CreateVacature(
        [FromBody] CreateVacatureCommand command, 
        CancellationToken cancellationToken)
    {
        Log.Information("Creating new vacature: {FunctionTitle}", command.FunctionTitle);
        var result = await _dispatcher.DispatchAsync<CreateVacatureCommand, VacatureDto>(command, cancellationToken);
        return CreatedAtAction(nameof(CreateVacature), new { id = result.VacatureId }, result);
    }
}