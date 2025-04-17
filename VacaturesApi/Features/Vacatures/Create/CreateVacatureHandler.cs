using AutoMapper;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;

namespace VacaturesApi.Features.Vacatures.Create;

/// <summary>
/// Handler for adding a vacature when passed a CreateVacatureCommand.
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
            UrlSlug = request.Vacature.UrlSlug,
            FunctionTitle = request.Vacature.FunctionTitle,  
            Availability = request.Vacature.Availability,  
            Location = request.Vacature.Location,  
            ContactPerson = request.Vacature.ContactPerson,  
            Description = request.Vacature.Description,  
            WhatToExpect = request.Vacature.WhatToExpect,  
            Responsibilities = request.Vacature.Responsibilities,  
            Offer = request.Vacature.Offer,  
            Requirements = request.Vacature.Requirements,  
            SalaryRange= request.Vacature.SalaryRange,  
            Industry = request.Vacature.Industry,  
            ListPriority = request.Vacature.ListPriority,
            Hidden = request.Vacature.Hidden,
            CreatedAt = DateTime.UtcNow, 
            UpdatedAt = request.Vacature.UpdatedAt
        };

        var createdVacature = await _repository.AddAsync(vacature, cancellationToken);
        return _mapper.Map<VacatureDto>(createdVacature);
    }
}