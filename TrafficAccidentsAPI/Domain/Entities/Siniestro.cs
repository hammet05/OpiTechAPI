using TrafficAccidentsAPI.Domain.Enums;
using TrafficAccidentsAPI.Domain.ValueObjects;

namespace TrafficAccidentsAPI.Domain.Entities
{
    public class Siniestro : BaseEntity
    {
        private readonly List<Vehiculo> _vehiculos = new();
        private readonly List<Victima> _victimas = new();

        public FechaHoraSiniestro FechaHora { get; private set; }
        public Ubicacion Ubicacion { get; private set; }
        public TipoSiniestro Tipo { get; private set; }
        public string? Descripcion { get; private set; }
        public IReadOnlyCollection<Vehiculo> Vehiculos => _vehiculos.AsReadOnly();
        public IReadOnlyCollection<Victima> Victimas => _victimas.AsReadOnly();
        public int NumeroVictimas => _victimas.Count;


        private Siniestro() { }


        public Siniestro(
            FechaHoraSiniestro fechaHora,
            Ubicacion ubicacion,
            TipoSiniestro tipo,
            string? descripcion = null)
        {
            FechaHora = fechaHora ?? throw new ArgumentNullException(nameof(fechaHora));
            Ubicacion = ubicacion ?? throw new ArgumentNullException(nameof(ubicacion));
            Tipo = tipo;
            Descripcion = descripcion;
        }


        public void AgregarVehiculo(TipoVehiculo tipo, string? placa = null)
        {
            var vehiculo = new Vehiculo(Id, tipo, placa);
            _vehiculos.Add(vehiculo);
        }

        public void AgregarVictima(string nombre, int edad, string condicion)
        {
            var victima = new Victima(Id, nombre, edad, condicion);
            _victimas.Add(victima);
        }

        public void ActualizarDescripcion(string descripcion)
        {
            Descripcion = descripcion;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ActualizarUbicacion(Ubicacion nuevaUbicacion)
        {
            Ubicacion = nuevaUbicacion ?? throw new ArgumentNullException(nameof(nuevaUbicacion));
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
