namespace TrafficAccidentsAPI.Domain.ValueObjects
{
    public class Ubicacion : IEquatable<Ubicacion>
    {
        public string Departamento { get; private set; }
        public string Ciudad { get; private set; }

        public Ubicacion(string departamento, string ciudad)
        {
            if (string.IsNullOrWhiteSpace(departamento))
                throw new ArgumentException("El departamento no puede estar vacío", nameof(departamento));

            if (string.IsNullOrWhiteSpace(ciudad))
                throw new ArgumentException("La ciudad no puede estar vacía", nameof(ciudad));

            Departamento = departamento.Trim();
            Ciudad = ciudad.Trim();
        }
        public bool Equals(Ubicacion? other)
        {
            if (other is null) return false;

            if (ReferenceEquals(this, other)) return true;

            return Departamento == other.Departamento && Ciudad == other.Ciudad;
        }

        public override bool Equals(object? obj) => Equals(obj as Ubicacion);
        public override int GetHashCode() => HashCode.Combine(Departamento, Ciudad);

        public static bool operator ==(Ubicacion? left, Ubicacion? right) => Equals(left, right);

        public static bool operator !=(Ubicacion? left, Ubicacion? right) => !Equals(left, right);
    }
}
