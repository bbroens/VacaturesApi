using MediatR;

namespace VacaturesApi.Features.Authentication.Login;

/// <summary>
/// Command record describing a request to log in as an API user.
/// Tells Mediatr to pass the LoginDto from the request into the handler listening for it.
/// Tells Mediatr that this request must return a AuthResponseDto.
/// </summary>

public record LoginCommand(LoginDto LoginDto) : IRequest<AuthResponseDto>;