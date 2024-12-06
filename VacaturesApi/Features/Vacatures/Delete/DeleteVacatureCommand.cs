using MediatR;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Command record describing a request to delete a vacature by id.
/// Tells Mediatr to pass the Guid from the request into the handler listening for it.
/// Tells Mediatr nothing about the requests expected response type.
/// </summary>

public record DeleteVacatureCommand(Guid VacatureId) : IRequest;