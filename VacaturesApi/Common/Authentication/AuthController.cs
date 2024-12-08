using Microsoft.AspNetCore.Mvc;

namespace VacaturesApi.Common.Authentication;

/// <summary>
/// Controller that handles API authentication requests.
/// </summary>

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto model)
    {
        try
        {
            var result = await _authService.Register(model);
            return CreatedAtAction(nameof(Register), result);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto model)
    {
        try
        {
            var result = await _authService.Login(model);
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid login attempt" });
        }
    }
}