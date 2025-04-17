namespace VacaturesApi.Common; 

/// <summary>
/// Empty response for requests that do not return a value.
/// </summary>

public sealed record EmptyResponse
{
    private EmptyResponse() { }
    public static EmptyResponse Instance { get; } = new();
}