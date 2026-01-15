namespace TrafficAccidentsAPI.Application.Dtos
{
    public record VehiculoDto
    {
        public Guid Id { get; init; }
        public string? Tipo { get; init; }
        public string? Placa { get; init; }
    }
}
