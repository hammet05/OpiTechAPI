using FluentValidation;
using TrafficAccidentsAPI.Domain.Enums;

namespace TrafficAccidentsAPI.Application.Commands.Validators
{
    public class VehiculoCommandValidator : AbstractValidator<VehiculoCommand>
    {
        public VehiculoCommandValidator()
        {
            RuleFor(x => x.Tipo).Must(tipo => Enum.IsDefined(typeof(TipoSiniestro), tipo)).WithMessage("Tipo de siniestro inválido");


            RuleFor(x => x.Placa).MaximumLength(20).When(x => !string.IsNullOrEmpty(x.Placa));
        }
    }
}
