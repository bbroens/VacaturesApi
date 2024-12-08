using System.ComponentModel.DataAnnotations;

namespace VacaturesApi.Features.Authentication.Register;

/// <summary>
/// DTO structure describing an API user registration request.
/// </summary>

public record RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; init; } = string.Empty;

    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public IEnumerable<string>? Roles { get; set; }
}