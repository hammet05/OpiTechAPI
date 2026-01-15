using TrafficAccidentsAPI.Domain.Enums;

namespace TrafficAccidentsAPI.Domain.Entities
{
    public class Vehiculo : BaseEntity
    {
        public Guid SiniestroId { get; private set; }
        public TipoVehiculo Tipo { get; private set; }
        public string? Placa { get; private set; }

        private Vehiculo() { }

        public Vehiculo(Guid siniestroId, TipoVehiculo tipo, string? placa = null)
        {
            SiniestroId = siniestroId;
            Tipo = tipo;
            Placa = placa;
        }

        public void ActualizarPlaca(string placa)
        {
            Placa = placa;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
