using AutoMapper;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Handler for updating a vacature when passed an UpdateVacatureCommand.
/// </summary>

public class UpdateVacatureHandler : IRequestHandler<UpdateVacatureCommand, VacatureDto>
{
    private readonly IVacatureRepository _repository;
    private readonly IMapper _mapper;

    public UpdateVacatureHandler(IVacatureRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<VacatureDto> Handle(UpdateVacatureCommand request, CancellationToken cancellationToken)
    {
        // Get the existing EF entity by id
        var existingVacature = 
            await _repository.GetByIdAsync(request.UpdateVacatureDto.VacatureId, cancellationToken)
            ?? throw new NotFoundException(nameof(Vacature), request.UpdateVacatureDto.VacatureId);

        // Map property values from DTO to the EF entity
        var updatedVacature = _mapper.Map(request.UpdateVacatureDto, existingVacature);

        // Update the vacature EF entity
        await _repository.UpdateAsync(updatedVacature, cancellationToken);

        // Return the updated EF entity as a DTO
        return _mapper.Map<VacatureDto>(updatedVacature);
    }
}