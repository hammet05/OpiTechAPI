namespace TrafficAccidentsAPI.Domain.Entities
{
    public class Victima : BaseEntity
    {
        public Guid SiniestroId { get; private set; }
        public string? Nombre { get; private set; }
        public int Edad { get; private set; }
        public string? Condicion { get; private set; }

        private Victima() { }

        public Victima(Guid siniestroId, string nombre, int edad, string condicion)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío", nameof(nombre));

            if (edad < 0 || edad > 100)
                throw new ArgumentException("La edad debe estar entre 0 y 100", nameof(edad));

            SiniestroId = siniestroId;
            Nombre = nombre;
            Edad = edad;
            Condicion = condicion;
        }
    }
}
