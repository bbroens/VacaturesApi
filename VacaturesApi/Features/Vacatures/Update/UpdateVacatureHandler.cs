using Mapster;
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

    public UpdateVacatureHandler(IVacatureRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<VacatureDto> Handle(UpdateVacatureCommand request, CancellationToken cancellationToken)
    {
        // Get the existing EF entity by id
        var existingVacature = 
            await _repository.GetByIdAsync(request.UpdateVacatureDto.VacatureId, cancellationToken)
            ?? throw new NotFoundException(nameof(Vacature), request.UpdateVacatureDto.VacatureId);

        // Map updated properties to the existing entity
        request.UpdateVacatureDto.Adapt(existingVacature);

        // Update the vacature EF entity
        await _repository.UpdateAsync(existingVacature, cancellationToken);

        // Return the updated EF entity as a VacatureDto
        return existingVacature.Adapt<VacatureDto>();
    }
}