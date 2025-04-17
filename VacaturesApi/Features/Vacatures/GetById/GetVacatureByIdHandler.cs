using Mapster;
using VacaturesApi.Common.Exceptions;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Domain;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Handler for fetching a vacature by id when passed a GetVacatureByIdQuery.
/// </summary>

public class GetVacatureByIdHandler : IRequestHandler<GetVacatureByIdQuery, VacatureDto>
{
    private readonly IVacatureRepository _repository;

    public GetVacatureByIdHandler(IVacatureRepository repository)
    {
        _repository = repository;
    }

    public async Task<VacatureDto> Handle(GetVacatureByIdQuery request, CancellationToken cancellationToken)
    {
        var vacature = await _repository.GetByIdAsync(request.VacatureId, cancellationToken) 
                       ?? throw new NotFoundException(nameof(Vacature), request.VacatureId);
        
        return vacature.Adapt<VacatureDto>();
    }
}