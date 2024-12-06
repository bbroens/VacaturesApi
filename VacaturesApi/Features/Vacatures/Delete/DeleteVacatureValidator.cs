using FluentValidation;

namespace VacaturesApi.Features.Vacatures.Delete;

public class DeleteVacatureValidator : AbstractValidator<DeleteVacatureCommand>
{
    public DeleteVacatureValidator()
    {
        RuleFor(x => x.VacatureId)
            .NotEmpty().WithMessage("VacatureId is required");
    }
}