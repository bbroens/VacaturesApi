using MediatR;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Query record describing a request to return a VacatureDto by id.
/// Uses Mediatr to pass the VacatureId to the handler listening.
/// </summary>

public record GetVacatureByIdQuery(Guid VacatureId) : IRequest<VacatureDto>;