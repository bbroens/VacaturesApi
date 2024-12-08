using System.ComponentModel.DataAnnotations;

namespace VacaturesApi.Common.Authentication;

/// <summary>
/// DTO structure for an API login request.
/// </summary>

public record LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}