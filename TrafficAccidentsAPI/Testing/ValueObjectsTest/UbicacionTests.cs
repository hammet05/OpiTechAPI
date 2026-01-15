using TrafficAccidentsAPI.Domain.ValueObjects;
using Xunit;

namespace TrafficAccidentsAPI.Testing.ValueObjectsTest
{
    public class UbicacionTests
    {
        [Fact]
        public void CrearUbicacion_ConDatosValidos_DebeCrearCorrectamente()
        {
            // Arrange & Act
            var ubicacion = new Ubicacion("Antioquia", "Medellín");

            // Assert
            Assert.Equal("Antioquia", ubicacion.Departamento);
            Assert.Equal("Medellín", ubicacion.Ciudad);
        }

        [Theory]
        [InlineData("", "Ciudad")]
        [InlineData("Departamento", "")]
        [InlineData(null, "Ciudad")]
        [InlineData("Departamento", null)]
        public void CrearUbicacion_ConDatosInvalidos_DebeLanzarExcepcion(
            string departamento,
            string ciudad)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Ubicacion(departamento, ciudad));
        }

        [Fact]
        public void UbicacionesConMismosValores_DebenSerIguales()
        {
            // Arrange
            var ubicacion1 = new Ubicacion("Valle del Cauca", "Cali");
            var ubicacion2 = new Ubicacion("Valle del Cauca", "Cali");

            // Act & Assert
            Assert.Equal(ubicacion1, ubicacion2);
            Assert.True(ubicacion1 == ubicacion2);
            Assert.False(ubicacion1 != ubicacion2);
        }

        [Fact]
        public void UbicacionesConDiferentesValores_NoDebenSerIguales()
        {
            // Arrange
            var ubicacion1 = new Ubicacion("Atlántico", "Barranquilla");
            var ubicacion2 = new Ubicacion("Bolívar", "Cartagena");

            // Act & Assert
            Assert.NotEqual(ubicacion1, ubicacion2);
            Assert.False(ubicacion1 == ubicacion2);
            Assert.True(ubicacion1 != ubicacion2);
        }
    }
}
