using MediatR;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Command record describing a request to update a vacature by id.
/// Tells Mediatr to pass the UpdateVacatureDto from the request into the handler listening for it.
/// Tells Mediatr that this request must return a VacatureDto.
/// </summary>

public record UpdateVacatureCommand(UpdateVacatureDto UpdateVacatureDto) : IRequest<VacatureDto>;