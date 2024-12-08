using Microsoft.AspNetCore.Mvc;

namespace VacaturesApi.Features.Authentication.Register;

/// <summary>
/// Endpoint for registering a new API user.
/// </summary>

[Route("api/auth")]
public class RegisterEndpoint : ControllerBase
{
    private readonly AuthRepository _authRepository;

    public RegisterEndpoint(AuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
   
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto model)
    {
        try
        {
            var result = await _authRepository.Register(model);
            return CreatedAtAction(nameof(Register), result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}