using System.ComponentModel.DataAnnotations;

namespace VacaturesApi.Common.Authentication;

/// <summary>
/// DTO structure to submit an API registration request.
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