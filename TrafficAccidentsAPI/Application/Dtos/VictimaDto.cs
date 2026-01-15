namespace TrafficAccidentsAPI.Application.Dtos
{
    public record VictimaDto
    {
        public Guid Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public int Edad { get; init; }
        public string Condicion { get; init; } = string.Empty;
    }
}
