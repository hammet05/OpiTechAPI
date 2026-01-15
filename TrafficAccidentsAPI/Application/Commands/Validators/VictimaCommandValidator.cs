using FluentValidation;

namespace TrafficAccidentsAPI.Application.Commands.Validators
{
    public class VictimaCommandValidator : AbstractValidator<VictimaCommand>
    {
        public VictimaCommandValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);

            RuleFor(x => x.Edad).GreaterThanOrEqualTo(0).LessThanOrEqualTo(105);

            RuleFor(x => x.Condicion).NotEmpty().MaximumLength(100);
        }
    }
}
