using Microsoft.AspNetCore.Mvc;
using VacaturesApi.Common.Dispatcher;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Endpoint for fetching a vacature by its id.
/// </summary>

[Route("api/vacatures")]
public class GetVacatureByIdEndpoint : ControllerBase
{
    private readonly Dispatcher _dispatcher;

    public GetVacatureByIdEndpoint(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{vacatureId:guid}")]
    [ProducesResponseType(typeof(VacatureDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VacatureDto>> GetVacatureById(
        Guid vacatureId, 
        CancellationToken cancellationToken)
    {
        var query = new GetVacatureByIdQuery(vacatureId);
        var result = await _dispatcher.DispatchAsync<GetVacatureByIdQuery, VacatureDto>(query, cancellationToken);
        return Ok(result);
    }
}