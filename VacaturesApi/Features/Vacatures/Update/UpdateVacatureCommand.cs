using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Command record describing a request to update a vacature and return the VacatureDto.
/// </summary>

public record UpdateVacatureCommand(UpdateVacatureDto UpdateVacatureDto) : IRequest<VacatureDto>;