using FluentValidation;
using TrafficAccidentsAPI.Domain.Enums;

namespace TrafficAccidentsAPI.Application.Commands.Validators
{
    public class RegistrarSiniestroCommandValidator : AbstractValidator<RegistrarSiniestroCommand>
    {
        public RegistrarSiniestroCommandValidator()
        {
            RuleFor(x => x.FechaHora).NotEmpty().LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha del siniestro no puede ser futura");

            RuleFor(x => x.Departamento).NotEmpty().MaximumLength(100);

            RuleFor(x => x.Ciudad).NotEmpty().MaximumLength(100);

            RuleFor(x => x.TipoSiniestro).Must(tipo => Enum.IsDefined(typeof(TipoSiniestro), tipo)).WithMessage("Tipo de siniestro inválido");


            RuleFor(x => x.Descripcion).MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Descripcion));

            RuleForEach(x => x.Vehiculos).SetValidator(new VehiculoCommandValidator());

            RuleForEach(x => x.Victimas).SetValidator(new VictimaCommandValidator());
        }
    }
}
