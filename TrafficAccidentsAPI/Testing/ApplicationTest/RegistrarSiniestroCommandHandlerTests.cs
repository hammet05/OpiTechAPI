using AutoMapper;
using Moq;
using TrafficAccidentsAPI.Application.Commands;
using TrafficAccidentsAPI.Application.Commands.Handlers;
using TrafficAccidentsAPI.Application.Dtos;
using TrafficAccidentsAPI.Domain.Entities;
using TrafficAccidentsAPI.Domain.Interfaces;
using Xunit;

namespace TrafficAccidentsAPI.Testing.ApplicationTest
{
    public class RegistrarSiniestroCommandHandlerTests
    {
        private readonly Mock<ISiniestroRepository> _mockRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public RegistrarSiniestroCommandHandlerTests()
        {
            _mockRepository = new Mock<ISiniestroRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_ConComandoValido_DebeRegistrarSiniestro()
        {
            // Arrange
            var command = new RegistrarSiniestroCommand
            {
                FechaHora = DateTime.UtcNow.AddHours(-1),
                Departamento = "Cundinamarca",
                Ciudad = "Bogotá",
                TipoSiniestro = 1,
                Descripcion = "Colisión en Autopista Norte",
                Vehiculos = new List<VehiculoCommand>
                {
                    new() { Tipo = 1, Placa = "ABC123" }
                },

                Victimas = new List<VictimaCommand>
                {
                    new() { Nombre = "Juan Pérez", Edad = 35, Condicion = "Leve" }
                }
            };

            var siniestroEsperado = new SiniestroDto
            {
                Id = Guid.NewGuid(),
                FechaHora = command.FechaHora,
                Departamento = command.Departamento,
                Ciudad = command.Ciudad
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<Siniestro>(), It.IsAny<CancellationToken>())).ReturnsAsync((Siniestro s, CancellationToken ct) => s);

            _mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            _mockMapper.Setup(m => m.Map<SiniestroDto>(It.IsAny<Siniestro>())).Returns(siniestroEsperado);

            var handler = new RegistrarSiniestroCommandHandler(_mockRepository.Object, _mockUnitOfWork.Object, _mockMapper.Object);

            // Act
            var resultado = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(command.Departamento, resultado.Departamento);
            Assert.Equal(command.Ciudad, resultado.Ciudad);

            _mockRepository.Verify(r => r.AddAsync(It.IsAny<Siniestro>(), It.IsAny<CancellationToken>()), Times.Once);

            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
