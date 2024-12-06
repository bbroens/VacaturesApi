using MediatR;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Handler for deleting a vacature
/// </summary>

public class DeleteVacatureHandler : IRequestHandler<DeleteVacatureCommand>
{
    private readonly IVacatureRepository _repository;

    public DeleteVacatureHandler(IVacatureRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(DeleteVacatureCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.VacatureId, cancellationToken);
    }
}