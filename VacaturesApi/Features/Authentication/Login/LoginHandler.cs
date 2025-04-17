using AutoMapper;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Authentication.Login;

/// <summary>
/// Handler for LoginCommand requests, used to log in as an API user.
/// </summary>

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly AuthRepository _authRepository;

    public LoginHandler(AuthRepository repository, IMapper mapper)
    {
        _authRepository = repository;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.Login(request.LoginDto);
    }
}