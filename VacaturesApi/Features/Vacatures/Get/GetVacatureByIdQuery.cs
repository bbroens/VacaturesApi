using MediatR;

namespace VacaturesApi.Features.Vacatures.Get;

public record GetVacatureByIdQuery(Guid VacatureId) : IRequest<VacatureDto>;
