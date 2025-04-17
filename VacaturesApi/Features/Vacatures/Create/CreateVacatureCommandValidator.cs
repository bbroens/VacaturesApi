using FluentValidation;

namespace VacaturesApi.Features.Vacatures.Create;

/// <summary>
/// Validator for the CreateVacatureCommand
/// </summary>

public class CreateVacatureCommandValidator : AbstractValidator<CreateVacatureCommand>
{
    public CreateVacatureCommandValidator()
    {
        RuleFor(x => x.Vacature.UrlSlug)
            .NotEmpty().WithMessage("URL slug is required")
            .MaximumLength(256).WithMessage("URL slug cannot exceed 256 characters");

        RuleFor(x => x.Vacature.FunctionTitle)
            .NotEmpty().WithMessage("Function title is required")
            .MaximumLength(128).WithMessage("Function title cannot exceed 128 characters");

        RuleFor(x => x.Vacature.Availability)
            .NotEmpty().WithMessage("Availability is required")
            .MaximumLength(32).WithMessage("Availability cannot exceed 32 characters");

        RuleFor(x => x.Vacature.Location)
            .NotEmpty().WithMessage("Location is required")
            .MaximumLength(128).WithMessage("Location cannot exceed 128 characters");

        RuleFor(x => x.Vacature.ContactPerson)
            .NotEmpty().WithMessage("Contact person is required")
            .MaximumLength(64).WithMessage("Contact person cannot exceed 64 characters");

        RuleFor(x => x.Vacature.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Vacature.WhatToExpect)
            .NotEmpty().WithMessage("What to expect is required");

        RuleFor(x => x.Vacature.Responsibilities)
            .NotEmpty().WithMessage("Responsibilities are required");

        RuleFor(x => x.Vacature.Offer)
            .NotEmpty().WithMessage("Offer is required");

        RuleFor(x => x.Vacature.Requirements)
            .NotEmpty().WithMessage("Requirements are required");

        RuleFor(x => x.Vacature.SalaryRange)
            .MaximumLength(32).WithMessage("Salary range cannot exceed 32 characters");

        RuleFor(x => x.Vacature.Industry)
            .MaximumLength(128).WithMessage("Industry cannot exceed 128 characters");

        RuleFor(x => x.Vacature.ListPriority)
            .GreaterThanOrEqualTo(0).WithMessage("List priority must be 0 or greater");

        RuleFor(x => x.Vacature.CreatedAt)
            .NotEmpty().WithMessage("Creation date is required")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Creation date cannot be in the future");
    }
}