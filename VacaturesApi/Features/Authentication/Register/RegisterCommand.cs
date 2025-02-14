using MediatR;

namespace VacaturesApi.Features.Authentication.Register;

/// <summary>
/// Command record describing a request to register a new API user and return an AuthResponseDto.
/// Uses Mediatr to pass the request RegisterDto to the handler listening.
/// </summary>

public record RegisterCommand(RegisterDto RegisterDto) : IRequest<AuthResponseDto>;