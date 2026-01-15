using TrafficAccidentsAPI.Domain.Entities;
using TrafficAccidentsAPI.Domain.Enums;
using TrafficAccidentsAPI.Domain.ValueObjects;
using Xunit;

namespace TrafficAccidentsAPI.Testing.DomainTest
{
    public class SiniestroTests
    {
        [Fact]
        public void CrearSiniestro_ConDatosValidos_DebeCrearCorrectamente()
        {
            // Arrange
            var fechaHora = new FechaHoraSiniestro(DateTime.UtcNow.AddHours(-1));
            var ubicacion = new Ubicacion("Cundinamarca", "Bogotá");
            var tipo = TipoSiniestro.Colision;
            var descripcion = "Colisión frontal en la Autopista Norte";

            // Act
            var siniestro = new Siniestro(fechaHora, ubicacion, tipo, descripcion);

            // Assert
            Assert.NotEqual(Guid.Empty, siniestro.Id);
            Assert.Equal(fechaHora, siniestro.FechaHora);
            Assert.Equal(ubicacion, siniestro.Ubicacion);
            Assert.Equal(tipo, siniestro.Tipo);
            Assert.Equal(descripcion, siniestro.Descripcion);
        }

        [Fact]
        public void AgregarVehiculo_DebeAgregarVehiculoAlSiniestro()
        {
            // Arrange
            var siniestro = CrearSiniestroValido();

            // Act
            siniestro.AgregarVehiculo(TipoVehiculo.Automovil, "ABC123");

            // Assert
            Assert.Single(siniestro.Vehiculos);
            Assert.Equal(TipoVehiculo.Automovil, siniestro.Vehiculos.First().Tipo);
            Assert.Equal("ABC123", siniestro.Vehiculos.First().Placa);
        }

        [Fact]
        public void AgregarVictima_DebeAgregarVictimaAlSiniestro()
        {
            // Arrange
            var siniestro = CrearSiniestroValido();

            // Act
            siniestro.AgregarVictima("Juan Pérez", 35, "Lesiones menores");

            // Assert
            Assert.Single(siniestro.Victimas);
            Assert.Equal(1, siniestro.NumeroVictimas);
            Assert.Equal("Juan Pérez", siniestro.Victimas.First().Nombre);
        }

        [Fact]
        public void ActualizarDescripcion_DebeActualizarDescripcionYFechaModificacion()
        {
            // Arrange
            var siniestro = CrearSiniestroValido();
            var descripcionOriginal = siniestro.Descripcion;

            // Act
            siniestro.ActualizarDescripcion("Nueva descripción detallada");

            // Assert
            Assert.NotEqual(descripcionOriginal, siniestro.Descripcion);
            Assert.Equal("Nueva descripción detallada", siniestro.Descripcion);
            Assert.NotNull(siniestro.UpdatedAt);
        }

        private Siniestro CrearSiniestroValido()
        {
            var fechaHora = new FechaHoraSiniestro(DateTime.UtcNow.AddHours(-1));
            var ubicacion = new Ubicacion("Cundinamarca", "Bogotá");
            return new Siniestro(fechaHora, ubicacion, TipoSiniestro.Colision);
        }
    }
}
