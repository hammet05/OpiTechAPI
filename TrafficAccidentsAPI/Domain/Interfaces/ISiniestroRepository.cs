using TrafficAccidentsAPI.Domain.Entities;

namespace TrafficAccidentsAPI.Domain.Interfaces
{
    public interface ISiniestroRepository
    {
        Task<Siniestro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<(IEnumerable<Siniestro> Items, int TotalCount)> GetPagedAsync(
            string? departamento,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        Task<Siniestro> AddAsync(Siniestro siniestro, CancellationToken cancellationToken = default);

        Task UpdateAsync(Siniestro siniestro, CancellationToken cancellationToken = default);

        Task DeleteAsync(Siniestro siniestro, CancellationToken cancellationToken = default);
    }
}
