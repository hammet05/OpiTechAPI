using MediatR;
using TrafficAccidentsAPI.Application.Dtos;

namespace TrafficAccidentsAPI.Application.Commands
{
    public record RegistrarSiniestroCommand : IRequest<SiniestroDto>
    {
        public DateTime FechaHora { get; init; }
        public string Departamento { get; init; } = string.Empty;
        public string Ciudad { get; init; } = string.Empty;
        public int TipoSiniestro { get; init; }
        public string? Descripcion { get; init; }
        public List<VehiculoCommand> Vehiculos { get; init; } = new();
        public List<VictimaCommand> Victimas { get; init; } = new();
    }
}
