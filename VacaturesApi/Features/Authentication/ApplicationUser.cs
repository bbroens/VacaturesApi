using Microsoft.AspNetCore.Identity;

namespace VacaturesApi.Features.Authentication;

/// <summary>
/// Base class for API users using Identity Framework
/// </summary>

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}