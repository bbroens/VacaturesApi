namespace VacaturesApi.Features.Authentication;

/// <summary>
/// DTO describing an authentication response outbound from the auth endpoint.
/// </summary>

public record AuthResponseDto
{
    public string Token { get; init; } = string.Empty;
    public DateTime Expiration { get; init; }
}