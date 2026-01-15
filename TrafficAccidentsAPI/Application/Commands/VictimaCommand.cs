namespace TrafficAccidentsAPI.Application.Commands
{
    public record VictimaCommand
    {
        public string Nombre { get; init; } = string.Empty;
        public int Edad { get; init; }
        public string Condicion { get; init; } = string.Empty;
    }
}
