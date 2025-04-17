using VacaturesApi.Common;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Command record describing a request to delete a vacature by id.
/// </summary>

public record DeleteVacatureCommand(Guid VacatureId) : IRequest<EmptyResponse>;