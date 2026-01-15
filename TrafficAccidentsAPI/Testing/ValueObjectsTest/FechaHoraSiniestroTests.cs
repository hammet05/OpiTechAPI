using TrafficAccidentsAPI.Domain.ValueObjects;
using Xunit;

namespace TrafficAccidentsAPI.Testing.ValueObjectsTest
{
    public class FechaHoraSiniestroTests
    {
        [Fact]
        public void CrearFechaHoraSiniestro_ConFechaValida_DebeCrearCorrectamente()
        {
            // Arrange
            var fecha = DateTime.UtcNow.AddHours(-2);

            // Act
            var fechaHora = new FechaHoraSiniestro(fecha);

            // Assert
            Assert.Equal(fecha, fechaHora.Valor);
        }

        [Fact]
        public void CrearFechaHoraSiniestro_ConFechaFutura_DebeLanzarExcepcion()
        {
            // Arrange
            var fechaFutura = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new FechaHoraSiniestro(fechaFutura));
        }

        [Fact]
        public void CrearFechaHoraSiniestro_ConFechaMuyAntigua_DebeLanzarExcepcion()
        {
            // Arrange
            var fechaAntigua = DateTime.UtcNow.AddYears(-11);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new FechaHoraSiniestro(fechaAntigua));
        }
    }
}
