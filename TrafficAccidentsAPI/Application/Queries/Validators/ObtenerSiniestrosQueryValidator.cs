using FluentValidation;

namespace TrafficAccidentsAPI.Application.Queries.Validators
{
    public class ObtenerSiniestrosQueryValidator : AbstractValidator<ObtenerSiniestrosQuery>
    {
        public ObtenerSiniestrosQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);

            RuleFor(x => x.FechaInicio).LessThanOrEqualTo(x => x.FechaFin).When(x => x.FechaInicio.HasValue && x.FechaFin.HasValue).WithMessage("La fecha de inicio debe ser menor o igual a la fecha fin");
        }
    }
}
