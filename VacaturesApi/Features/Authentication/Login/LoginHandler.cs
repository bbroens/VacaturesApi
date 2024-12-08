using AutoMapper;
using MediatR;

namespace VacaturesApi.Features.Authentication.Login;

/// <summary>
/// Handler for logging in as an API user.
/// Listens to command of type LoginCommand through MediatR. 
/// </summary>

public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly AuthRepository _authRepository;
    private readonly IMapper _mapper;

    public LoginHandler(AuthRepository repository, IMapper mapper)
    {
        _authRepository = repository;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.Login(request.LoginDto);
    }
}