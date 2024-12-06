using MediatR;

namespace VacaturesApi.Features.Vacatures.GetById;

public record GetVacatureByIdQuery(Guid VacatureId) : IRequest<VacatureDto>;
