using AutoMapper;
using MediatR;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;

namespace VacaturesApi.Features.Vacatures.Create;

/// <summary>
/// Handler for adding a vacature.
/// Listens to command of type CreateVacatureCommand through MediatR. 
/// </summary>

public class CreateVacatureHandler : IRequestHandler<CreateVacatureCommand, VacatureDto>
{
    private readonly IVacatureRepository _repository;
    private readonly IMapper _mapper;

    public CreateVacatureHandler(IVacatureRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<VacatureDto> Handle(CreateVacatureCommand request, CancellationToken cancellationToken)
    {
        var vacature = new Vacature 
        {
            VacatureId = Guid.NewGuid(),
            UrlSlug = request.UrlSlug,
            FunctionTitle = request.FunctionTitle,  
            Availability = request.Availability,  
            Location = request.Location,  
            ContactPerson = request.ContactPerson,  
            Description = request.Description,  
            WhatToExpect = request.WhatToExpect,  
            Responsibilities = request.Responsibilities,  
            Offer = request.Offer,  
            Requirements = request.Requirements,  
            SalaryRange= request.SalaryRange,  
            Industry = request.Industry,  
            ListPriority = request.ListPriority,
            Hidden = request.Hidden,
            CreatedAt = DateTime.UtcNow, 
            UpdatedAt = request.UpdatedAt
        };

        var createdVacature = await _repository.AddAsync(vacature, cancellationToken);
        return _mapper.Map<VacatureDto>(createdVacature);
    }
}