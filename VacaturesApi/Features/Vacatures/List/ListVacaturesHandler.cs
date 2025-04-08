using AutoMapper;
using VacaturesApi.Common.Interfaces;
using VacaturesApi.Common.Pagination;

namespace VacaturesApi.Features.Vacatures.List;

/// <summary>
/// Handler for fetching a paginated list of vacatures when passed a ListVacaturesQuery.
/// </summary>

public class ListVacaturesHandler : IRequestHandler<ListVacaturesQuery, PaginatedResult<VacatureDto>>
{
    private readonly IVacatureRepository _repository;
    private readonly IMapper _mapper;

    public ListVacaturesHandler(IVacatureRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<PaginatedResult<VacatureDto>> Handle(ListVacaturesQuery request, CancellationToken cancellationToken)
    {
        var vacatures = await _repository.ListAsync(request.Page, request.PageSize, cancellationToken);
        var totalCount = await _repository.CountAsync(cancellationToken);

        return new PaginatedResult<VacatureDto>
        {
            Items = _mapper.Map<List<VacatureDto>>(vacatures),
            TotalCount = totalCount,
            PageNumber = request.Page,
            PageSize = request.PageSize
        };
    }
}