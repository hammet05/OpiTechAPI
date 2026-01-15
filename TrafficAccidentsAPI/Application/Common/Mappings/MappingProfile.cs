using AutoMapper;
using TrafficAccidentsAPI.Application.Dtos;
using TrafficAccidentsAPI.Domain.Entities;

namespace TrafficAccidentsAPI.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Siniestro, SiniestroDto>()
           .ForMember(d => d.FechaHora, opt => opt.MapFrom(s => s.FechaHora.Valor))
           .ForMember(d => d.Departamento, opt => opt.MapFrom(s => s.Ubicacion.Departamento))
           .ForMember(d => d.Ciudad, opt => opt.MapFrom(s => s.Ubicacion.Ciudad))
           .ForMember(d => d.TipoSiniestro, opt => opt.MapFrom(s => s.Tipo.ToString()))
           .ForMember(d => d.NumeroVehiculos, opt => opt.MapFrom(s => s.Vehiculos.Count))
           .ForMember(d => d.NumeroVictimas, opt => opt.MapFrom(s => s.NumeroVictimas))
           .ForMember(d => d.Vehiculos, opt => opt.MapFrom(s => s.Vehiculos))
           .ForMember(d => d.Victimas, opt => opt.MapFrom(s => s.Victimas));

            CreateMap<Vehiculo, VehiculoDto>().ForMember(d => d.Tipo, opt => opt.MapFrom(s => s.Tipo.ToString()));

            CreateMap<Victima, VictimaDto>();
        }
    }
}
