using VacaturesApi.Common;
using VacaturesApi.Common.Interfaces;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Handler for deleting a vacature when passed a DeleteVacatureCommand.
/// </summary>

public class DeleteVacatureHandler : IRequestHandler<DeleteVacatureCommand, EmptyResponse>
{
    private readonly IVacatureRepository _repository;

    public DeleteVacatureHandler(IVacatureRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<EmptyResponse> Handle(DeleteVacatureCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.VacatureId, cancellationToken);
        return EmptyResponse.Instance;
    }
}