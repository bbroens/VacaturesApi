using AutoMapper;
using MediatR;

namespace VacaturesApi.Features.Authentication.Register;

/// <summary>
/// Handler for registering a new API user.
/// Listens to command of type RegisterCommand through MediatR. 
/// </summary>

public class LoginHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly AuthRepository _authRepository;
    private readonly IMapper _mapper;

    public LoginHandler(AuthRepository repository, IMapper mapper)
    {
        _authRepository = repository;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.Register(request.RegisterDto);
    }
}