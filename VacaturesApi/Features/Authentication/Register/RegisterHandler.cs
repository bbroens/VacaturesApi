﻿using AutoMapper;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Authentication.Register;

/// <summary>
/// Handler for RegisterCommand requests, used to register a new API user.
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