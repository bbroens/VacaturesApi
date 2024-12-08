using System.ComponentModel.DataAnnotations;

namespace VacaturesApi.Features.Authentication.Login;

/// <summary>
/// DTO structure describing an API user login request.
/// </summary>

public record LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}