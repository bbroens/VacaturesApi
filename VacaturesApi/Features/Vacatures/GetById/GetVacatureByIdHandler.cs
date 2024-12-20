﻿using AutoMapper;
using MediatR;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Handler for fetching a vacature by its id.
/// Listens to query of type GetVacatureByIdQuery through MediatR. 
/// </summary>

public class GetVacatureByIdHandler : IRequestHandler<GetVacatureByIdQuery, VacatureDto>
{
    private readonly IVacatureRepository _repository;
    private readonly IMapper _mapper;

    public GetVacatureByIdHandler(IVacatureRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<VacatureDto> Handle(GetVacatureByIdQuery request, CancellationToken cancellationToken)
    {
        var vacature = await _repository.GetByIdAsync(request.VacatureId, cancellationToken) 
                       ?? throw new NotFoundException(nameof(Vacature), request.VacatureId);
        
        return _mapper.Map<VacatureDto>(vacature);
    }
}