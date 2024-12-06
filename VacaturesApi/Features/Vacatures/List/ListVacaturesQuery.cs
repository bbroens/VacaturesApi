using MediatR;

namespace VacaturesApi.Features.Vacatures.List;

/// <summary>
/// Query record describing a request to return a list of vacatures.
/// Carries no data for the handler listening for it.
/// Tells Mediatr that this request must return a List of VacatureDto.
/// </summary>

public record ListVacaturesQuery : IRequest<List<VacatureDto>>;