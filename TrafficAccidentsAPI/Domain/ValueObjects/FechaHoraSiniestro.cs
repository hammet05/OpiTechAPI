namespace TrafficAccidentsAPI.Domain.ValueObjects
{
    public class FechaHoraSiniestro : IEquatable<FechaHoraSiniestro>
    {
        public DateTime Valor { get; private set; }

        private FechaHoraSiniestro() { }

        public FechaHoraSiniestro(DateTime fechaHora)
        {
            if (fechaHora > DateTime.UtcNow)
                throw new ArgumentException("La fecha del siniestro no puede ser futura", nameof(fechaHora));

            if (fechaHora < DateTime.UtcNow.AddYears(-10))
                throw new ArgumentException("La fecha del siniestro no puede ser mayor a 10 años", nameof(fechaHora));

            Valor = fechaHora;
        }

        public bool Equals(FechaHoraSiniestro? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Valor == other.Valor;
        }

        public override bool Equals(object? obj) => Equals(obj as FechaHoraSiniestro);

        public override int GetHashCode() => Valor.GetHashCode();

        public static bool operator ==(FechaHoraSiniestro? left, FechaHoraSiniestro? right) => Equals(left, right);

        public static bool operator !=(FechaHoraSiniestro? left, FechaHoraSiniestro? right) => !Equals(left, right);
    }
}
