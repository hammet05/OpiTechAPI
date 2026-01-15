namespace TrafficAccidentsAPI.Application.Dtos
{
    public record SiniestroDto
    {
        public Guid Id { get; init; }
        public DateTime FechaHora { get; init; }
        public string Departamento { get; init; } = string.Empty;
        public string Ciudad { get; init; } = string.Empty;
        public string TipoSiniestro { get; init; } = string.Empty;
        public int NumeroVehiculos { get; init; }
        public int NumeroVictimas { get; init; }
        public string? Descripcion { get; init; }
        public List<VehiculoDto> Vehiculos { get; init; } = new();
        public List<VictimaDto> Victimas { get; init; } = new();
    }
}
