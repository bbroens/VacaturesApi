using FluentValidation;

namespace VacaturesApi.Features.Vacatures.Create;

/// <summary>
/// Validator for the CreateVacatureCommand
/// </summary>

public class CreateVacatureCommandValidator : AbstractValidator<CreateVacatureCommand>
{
    public CreateVacatureCommandValidator()
    {
        RuleFor(x => x.UrlSlug)
            .NotEmpty().WithMessage("URL slug is required")
            .MaximumLength(256).WithMessage("URL slug cannot exceed 256 characters");

        RuleFor(x => x.FunctionTitle)
            .NotEmpty().WithMessage("Function title is required")
            .MaximumLength(128).WithMessage("Function title cannot exceed 128 characters");

        RuleFor(x => x.Availability)
            .NotEmpty().WithMessage("Availability is required")
            .MaximumLength(32).WithMessage("Availability cannot exceed 32 characters");

        RuleFor(x => x.Location)
            .NotEmpty().WithMessage("Location is required")
            .MaximumLength(128).WithMessage("Location cannot exceed 128 characters");

        RuleFor(x => x.ContactPerson)
            .NotEmpty().WithMessage("Contact person is required")
            .MaximumLength(64).WithMessage("Contact person cannot exceed 64 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.WhatToExpect)
            .NotEmpty().WithMessage("What to expect is required");

        RuleFor(x => x.Responsibilities)
            .NotEmpty().WithMessage("Responsibilities are required");

        RuleFor(x => x.Offer)
            .NotEmpty().WithMessage("Offer is required");

        RuleFor(x => x.Requirements)
            .NotEmpty().WithMessage("Requirements are required");

        RuleFor(x => x.SalaryRange)
            .MaximumLength(32).WithMessage("Salary range cannot exceed 32 characters");

        RuleFor(x => x.Industry)
            .MaximumLength(128).WithMessage("Industry cannot exceed 128 characters");

        RuleFor(x => x.ListPriority)
            .GreaterThanOrEqualTo(0).WithMessage("List priority must be 0 or greater");

        RuleFor(x => x.CreatedAt)
            .NotEmpty().WithMessage("Creation date is required")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Creation date cannot be in the future");
    }
}