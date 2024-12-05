using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace VacaturesApi.Features.Vacatures.Create;

[Route("api/vacatures")]
public class CreateVacatureEndpoint : ControllerBase
{
    private readonly IMediator _mediator;

    public CreateVacatureEndpoint(IMediator mediator, ILogger<CreateVacatureEndpoint> logger)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(VacatureDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VacatureDto>> CreateVacature(
        [FromBody] CreateVacatureCommand command, 
        CancellationToken cancellationToken)
    {
        try
        {
            Log.Information("Creating new vacature: {FunctionTitle}", command.FunctionTitle);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(CreateVacature), new { id = result.VacatureId }, result);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error creating vacature");
            return BadRequest(ex.Message);
        }
    }
}