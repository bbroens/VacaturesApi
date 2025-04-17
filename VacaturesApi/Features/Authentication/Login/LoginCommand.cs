using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Authentication.Login;

/// <summary>
/// Command record describing a request to log in as an API user and return an AuthResponseDto.
/// </summary>

public abstract record LoginCommand(LoginDto LoginDto) : IRequest<AuthResponseDto>;