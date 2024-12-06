using FluentValidation;

namespace VacaturesApi.Features.Vacatures.GetById;

public class GetVacatureByIdQueryValidator : AbstractValidator<GetVacatureByIdQuery>
{
    public GetVacatureByIdQueryValidator()
    {
        RuleFor(x => x.VacatureId)
            .NotEmpty().WithMessage("Vacature ID is required");
    }
}