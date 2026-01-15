using AutoMapper;
using MediatR;
using TrafficAccidentsAPI.Application.Dtos;
using TrafficAccidentsAPI.Domain.Entities;
using TrafficAccidentsAPI.Domain.Enums;
using TrafficAccidentsAPI.Domain.Interfaces;
using TrafficAccidentsAPI.Domain.ValueObjects;

namespace TrafficAccidentsAPI.Application.Commands.Handlers
{
    public class RegistrarSiniestroCommandHandler : IRequestHandler<RegistrarSiniestroCommand, SiniestroDto>
    {
        private readonly ISiniestroRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegistrarSiniestroCommandHandler(ISiniestroRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SiniestroDto> Handle(RegistrarSiniestroCommand request, CancellationToken cancellationToken)
        {

            var fechaHora = new FechaHoraSiniestro(request.FechaHora);
            var ubicacion = new Ubicacion(request.Departamento, request.Ciudad);
            var tipo = (TipoSiniestro)request.TipoSiniestro;


            var siniestro = new Siniestro(fechaHora, ubicacion, tipo, request.Descripcion);

            foreach (var vehiculoCmd in request.Vehiculos)
            {
                var tipoVehiculo = (TipoVehiculo)vehiculoCmd.Tipo;
                siniestro.AgregarVehiculo(tipoVehiculo, vehiculoCmd.Placa);
            }

            foreach (var victimaCmd in request.Victimas)
            {
                siniestro.AgregarVictima(
                    victimaCmd.Nombre,
                    victimaCmd.Edad,
                    victimaCmd.Condicion);
            }


            await _repository.AddAsync(siniestro, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return _mapper.Map<SiniestroDto>(siniestro);
        }
    }
}
