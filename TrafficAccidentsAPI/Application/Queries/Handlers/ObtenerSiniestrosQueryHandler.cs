using AutoMapper;
using MediatR;
using TrafficAccidentsAPI.Application.Dtos;
using TrafficAccidentsAPI.Domain.Interfaces;

namespace TrafficAccidentsAPI.Application.Queries.Handlers
{
    public class ObtenerSiniestrosQueryHandler : IRequestHandler<ObtenerSiniestrosQuery, PagedResultDto<SiniestroDto>>
    {
        private readonly ISiniestroRepository _repository;
        private readonly IMapper _mapper;

        public ObtenerSiniestrosQueryHandler(ISiniestroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<SiniestroDto>> Handle(ObtenerSiniestrosQuery request, CancellationToken cancellationToken)
        {

            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 :
                           request.PageSize > 100 ? 100 : request.PageSize;


            var (items, totalCount) = await _repository.GetPagedAsync(request.Departamento, request.FechaInicio, request.FechaFin, pageNumber, pageSize, cancellationToken);


            var itemsDto = _mapper.Map<List<SiniestroDto>>(items);


            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new PagedResultDto<SiniestroDto>
            {
                Items = itemsDto,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount
            };
        }
    }
}
