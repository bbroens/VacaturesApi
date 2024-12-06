using FluentValidation;

namespace VacaturesApi.Features.Vacatures.Delete;

/// <summary>
/// Validator for the DeleteVacatureCommand
/// </summary>

public class DeleteVacatureValidator : AbstractValidator<DeleteVacatureCommand>
{
    public DeleteVacatureValidator()
    {
        RuleFor(x => x.VacatureId)
            .NotEmpty().WithMessage("VacatureId is required");
    }
}