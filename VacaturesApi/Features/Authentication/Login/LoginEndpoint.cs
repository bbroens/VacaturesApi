using Microsoft.AspNetCore.Mvc;

namespace VacaturesApi.Features.Authentication.Login;

/// <summary>
/// Endpoint for logging in as an API user.
/// </summary>

[Route("api/auth")]
public class LoginEndpoint : ControllerBase
{
    private readonly AuthRepository _authRepository;

    public LoginEndpoint(AuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
   
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto model)
    {
        try
        {
            var result = await _authRepository.Login(model);
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid login attempt" });
        }
    }
}