using FluentValidation.TestHelper;
using TrafficAccidentsAPI.Application.Commands;
using TrafficAccidentsAPI.Application.Commands.Validators;
using Xunit;

namespace TrafficAccidentsAPI.Testing.ValidatorsTest
{
    public class RegistrarSiniestroCommandValidatorTests
    {
        private readonly RegistrarSiniestroCommandValidator _validator;

        public RegistrarSiniestroCommandValidatorTests()
        {
            _validator = new RegistrarSiniestroCommandValidator();
        }

        [Fact]
        public void Validate_ConComandoValido_NoDebeGenerarErrores()
        {
            // Arrange
            var command = new RegistrarSiniestroCommand
            {
                FechaHora = DateTime.UtcNow.AddHours(-1),
                Departamento = "Cundinamarca",
                Ciudad = "Bogotá",
                TipoSiniestro = 1,
                Descripcion = "Descripción válida"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_ConFechaFutura_DebeGenerarError()
        {
            // Arrange
            var command = new RegistrarSiniestroCommand
            {
                FechaHora = DateTime.UtcNow.AddDays(1),
                Departamento = "Cundinamarca",
                Ciudad = "Bogotá",
                TipoSiniestro = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.FechaHora);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Validate_ConDepartamentoInvalido_DebeGenerarError(string departamento)
        {
            // Arrange
            var command = new RegistrarSiniestroCommand
            {
                FechaHora = DateTime.UtcNow.AddHours(-1),
                Departamento = departamento,
                Ciudad = "Bogotá",
                TipoSiniestro = 1
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Departamento);
        }

        [Fact]
        public void Validate_ConDescripcionMuyLarga_DebeGenerarError()
        {
            // Arrange
            var command = new RegistrarSiniestroCommand
            {
                FechaHora = DateTime.UtcNow.AddHours(-1),
                Departamento = "Cundinamarca",
                Ciudad = "Bogotá",
                TipoSiniestro = 1,
                Descripcion = new string('A', 1001) // Más de 1000 caracteres
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Descripcion);
        }
    }
}
