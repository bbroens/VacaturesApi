using MediatR;

namespace VacaturesApi.Features.Authentication.Register;

/// <summary>
/// Command record describing a request to register a new API user.
/// Tells Mediatr to pass the RegisterDto from the request into the handler listening for it.
/// Tells Mediatr that this request must return a AuthResponseDto.
/// </summary>

public record RegisterCommand(RegisterDto RegisterDto) : IRequest<AuthResponseDto>;