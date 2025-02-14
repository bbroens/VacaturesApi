using MediatR;

namespace VacaturesApi.Features.Authentication.Login;

/// <summary>
/// Command record describing a request to log in as an API user and return an AuthResponseDto.
/// Uses Mediatr to pass the request LoginDto to the handler listening.
/// </summary>

public record LoginCommand(LoginDto LoginDto) : IRequest<AuthResponseDto>;