using MediatR;

namespace VacaturesApi.Features.Vacatures.List;

public record ListVacaturesQuery : IRequest<List<VacatureDto>>;