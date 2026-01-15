namespace TrafficAccidentsAPI.Application.Commands
{
    public record VehiculoCommand
    {
        public int Tipo { get; init; }
        public string? Placa { get; init; }
    }
}
