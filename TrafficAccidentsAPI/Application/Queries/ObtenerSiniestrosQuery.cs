using MediatR;
using TrafficAccidentsAPI.Application.Dtos;

namespace TrafficAccidentsAPI.Application.Queries
{
    public record ObtenerSiniestrosQuery : IRequest<PagedResultDto<SiniestroDto>>
    {
        public string? Departamento { get; init; }
        public DateTime? FechaInicio { get; init; }
        public DateTime? FechaFin { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
