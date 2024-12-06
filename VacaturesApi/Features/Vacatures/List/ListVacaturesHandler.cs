using AutoMapper;
using MediatR;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.List;

/// <summary>
/// Handler for fetching a list of vacatures.
/// Listens to query of type ListVacaturesQuery through MediatR. 
/// </summary>

public class ListVacaturesHandler : IRequestHandler<ListVacaturesQuery, List<VacatureDto>>
{
    private readonly IVacatureRepository _repository;
    private readonly IMapper _mapper;

    public ListVacaturesHandler(IVacatureRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<VacatureDto>> Handle(ListVacaturesQuery request, CancellationToken cancellationToken)
    {
        var vacatures = await _repository.ListAsync(cancellationToken);
        return _mapper.Map<List<VacatureDto>>(vacatures);
    }
}