using MediatR;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Command record describing a request to return a vacature by id.
/// Tells Mediatr to pass the Guid from the request into the handler listening for it.
/// Tells Mediatr that this request must return a VacatureDto.
/// </summary>

public record GetVacatureByIdQuery(Guid VacatureId) : IRequest<VacatureDto>;