using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrafficAccidentsAPI.Application.Commands;
using TrafficAccidentsAPI.Application.Dtos;
using TrafficAccidentsAPI.Application.Queries;

namespace TrafficAccidentsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiniestrosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SiniestrosController> _logger;

        public SiniestrosController(IMediator mediator, ILogger<SiniestrosController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SiniestroDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SiniestroDto>> RegistrarSiniestro([FromBody] RegistrarSiniestroCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Registrando nuevo siniestro en {Departamento}, {Ciudad}",
                command.Departamento, command.Ciudad);

            var resultado = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(ObtenerSiniestroPorId), new { id = resultado.Id }, resultado);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<SiniestroDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResultDto<SiniestroDto>>> ObtenerSiniestros([FromQuery] string? departamento, [FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Consultando siniestros - Departamento: {Departamento}, Página: {PageNumber}", departamento ?? "Todos", pageNumber);

            var query = new ObtenerSiniestrosQuery
            {
                Departamento = departamento,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var resultado = await _mediator.Send(query, cancellationToken);

            return Ok(resultado);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SiniestroDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SiniestroDto>> ObtenerSiniestroPorId(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consultando siniestro con ID: {Id}", id);

            var query = new ObtenerSiniestrosQuery
            {
                PageNumber = 1,
                PageSize = 1
            };

            var resultado = await _mediator.Send(query, cancellationToken);
            var siniestro = resultado.Items.FirstOrDefault(s => s.Id == id);

            if (siniestro == null)
            {
                _logger.LogWarning("Siniestro con ID {Id} no encontrado", id);
                return NotFound(new { message = $"Siniestro con ID {id} no encontrado" });
            }

            return Ok(siniestro);
        }
    }
}
