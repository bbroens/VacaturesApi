using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Authentication.Register;

/// <summary>
/// Command record describing a request to register a new API user and return an AuthResponseDto.
/// </summary>

public abstract record RegisterCommand(RegisterDto RegisterDto) : IRequest<AuthResponseDto>;