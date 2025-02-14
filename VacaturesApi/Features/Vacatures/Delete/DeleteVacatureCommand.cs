using MediatR;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Command record describing a request to delete a vacature by id.
/// Uses Mediatr to pass the VacatureId to the handler listening.
/// </summary>

public record DeleteVacatureCommand(Guid VacatureId) : IRequest;