using MediatR;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Command record describing a request to update a vacature and return the VacatureDto.
/// Uses Mediatr to pass the request UpdateVacatureDto to the handler listening.
/// </summary>

public record UpdateVacatureCommand(UpdateVacatureDto UpdateVacatureDto) : IRequest<VacatureDto>;