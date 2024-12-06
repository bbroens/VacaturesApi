using MediatR;

namespace VacaturesApi.Features.Vacatures.Delete;

public record DeleteVacatureCommand(Guid VacatureId) : IRequest;