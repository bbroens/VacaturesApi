namespace VacaturesApi.Features.Authentication;

/// <summary>
/// DTO structure to return an API authentication response.
/// </summary>

public record AuthResponseDto
{
    public string Token { get; init; } = string.Empty;
    public DateTime Expiration { get; init; }
}