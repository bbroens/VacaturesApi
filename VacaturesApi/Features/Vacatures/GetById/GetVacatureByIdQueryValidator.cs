using FluentValidation;

namespace VacaturesApi.Features.Vacatures.GetById;

/// <summary>
/// Validator for the GetVacatureByIdQuery
/// </summary>

public class GetVacatureByIdQueryValidator : AbstractValidator<GetVacatureByIdQuery>
{
    public GetVacatureByIdQueryValidator()
    {
        RuleFor(x => x.VacatureId)
            .NotEmpty().WithMessage("VacatureId is required");
    }
}