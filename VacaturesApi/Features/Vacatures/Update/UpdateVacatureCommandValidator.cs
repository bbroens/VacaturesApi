using FluentValidation;

namespace VacaturesApi.Features.Vacatures.Update;

/// <summary>
/// Validator for the UpdateVacatureCommand.
/// </summary>

public class UpdateVacatureCommandValidator : AbstractValidator<UpdateVacatureDto>
{
    public UpdateVacatureCommandValidator()
    {
        // PUT only updates changed fields, so just the id is required.
        RuleFor(x => x.VacatureId)
            .NotEmpty().WithMessage("Vacature ID is required");

        When(x => !string.IsNullOrEmpty(x.UrlSlug), () => {
            RuleFor(x => x.UrlSlug)
                .MaximumLength(256).WithMessage("URL slug cannot exceed 256 characters");
        });

        When(x => !string.IsNullOrEmpty(x.FunctionTitle), () => {
            RuleFor(x => x.FunctionTitle)
                .MaximumLength(128).WithMessage("Function title cannot exceed 128 characters");
        });

        When(x => !string.IsNullOrEmpty(x.Availability), () => {
            RuleFor(x => x.Availability)
                .MaximumLength(32).WithMessage("Availability cannot exceed 32 characters");
        });

        When(x => !string.IsNullOrEmpty(x.Location), () => {
            RuleFor(x => x.Location)
                .MaximumLength(128).WithMessage("Location cannot exceed 128 characters");
        });

        When(x => !string.IsNullOrEmpty(x.ContactPerson), () => {
            RuleFor(x => x.ContactPerson)
                .MaximumLength(64).WithMessage("Contact person cannot exceed 64 characters");
        });

        When(x => x.ListPriority.HasValue, () => {
            RuleFor(x => x.ListPriority)
                .GreaterThanOrEqualTo(0).WithMessage("List priority must be 0 or greater");
        });

        When(x => !string.IsNullOrEmpty(x.SalaryRange), () => {
            RuleFor(x => x.SalaryRange)
                .MaximumLength(32).WithMessage("Salary range cannot exceed 32 characters");
        });

        When(x => !string.IsNullOrEmpty(x.Industry), () => {
            RuleFor(x => x.Industry)
                .MaximumLength(128).WithMessage("Industry cannot exceed 128 characters");
        });
    }
}