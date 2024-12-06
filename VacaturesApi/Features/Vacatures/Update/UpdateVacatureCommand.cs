using MediatR;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Command to update a vacature.
/// This command has its own UpdateDto to shape its request and response data.
/// The UpdateDto is mapped to the Vacature entity on Feature level.
/// </summary>

public record UpdateVacatureCommand(UpdateVacatureDto UpdateDto) 
    : IRequest<VacatureDto>;