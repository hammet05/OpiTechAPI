using Microsoft.EntityFrameworkCore;
using TrafficAccidentsAPI.Domain.Entities;
using TrafficAccidentsAPI.Domain.Interfaces;
using TrafficAccidentsAPI.Infrastructure.Persistence;

namespace TrafficAccidentsAPI.Infrastructure.Repositories
{
    public class SiniestroRepository : ISiniestroRepository
    {
        private readonly ApplicationDbContext _context;

        public SiniestroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Siniestro> AddAsync(Siniestro siniestro, CancellationToken cancellationToken = default)
        {
            await _context.Siniestros.AddAsync(siniestro, cancellationToken);
            return siniestro;
        }

        public async Task<Siniestro?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Siniestros
            .Include(s => s.Vehiculos)
            .Include(s => s.Victimas)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<(IEnumerable<Siniestro> Items, int TotalCount)> GetPagedAsync(string? departamento, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.Siniestros
             .Include(s => s.Vehiculos)
             .Include(s => s.Victimas)
             .AsQueryable();

            if (!string.IsNullOrWhiteSpace(departamento))
            {
                query = query.Where(s => s.Ubicacion.Departamento == departamento);
            }

            if (fechaInicio.HasValue)
            {
                query = query.Where(s => s.FechaHora.Valor >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                query = query.Where(s => s.FechaHora.Valor <= fechaFin.Value);
            }


            var totalCount = await query.CountAsync(cancellationToken);


            var items = await query
                .OrderByDescending(s => s.FechaHora.Valor)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        public Task UpdateAsync(Siniestro siniestro, CancellationToken cancellationToken = default)
        {
            _context.Siniestros.Update(siniestro);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Siniestro siniestro, CancellationToken cancellationToken = default)
        {
            _context.Siniestros.Remove(siniestro);
            return Task.CompletedTask;
        }
    }
}
