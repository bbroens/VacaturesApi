using FluentValidation;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Validator for the DeleteVacatureCommand
/// </summary>

public class DeleteVacatureCommandValidator : AbstractValidator<DeleteVacatureCommand>
{
    public DeleteVacatureCommandValidator()
    {
        RuleFor(x => x.VacatureId)
            .NotEmpty().WithMessage("VacatureId is required");
    }
}