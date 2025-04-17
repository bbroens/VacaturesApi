using System.ComponentModel.DataAnnotations;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.Create;

/// <summary>
/// Command record describing a request to create a VacatureDto and return it.
/// </summary>

public record CreateVacatureCommand(VacatureDto Vacature) : IRequest<VacatureDto>;