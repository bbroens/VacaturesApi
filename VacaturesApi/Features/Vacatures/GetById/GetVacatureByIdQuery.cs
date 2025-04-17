using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Query record describing a request to return a VacatureDto by id.
/// </summary>

public record GetVacatureByIdQuery(Guid VacatureId) : IRequest<VacatureDto>;