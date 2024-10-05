using FluentValidation;

namespace ParkLocator.Features.Regions.Create.Validator;

public class Validator : AbstractValidator<Command.Command>
{
    public Validator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(c => c.Country)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(c => c.StateProvince)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(c => c.Locality)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(200);
    }
}