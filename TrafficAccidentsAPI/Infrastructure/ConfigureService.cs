using Microsoft.EntityFrameworkCore;
using TrafficAccidentsAPI.Domain.Interfaces;
using TrafficAccidentsAPI.Infrastructure.Persistence;
using TrafficAccidentsAPI.Infrastructure.Repositories;

namespace TrafficAccidentsAPI.Infrastructure
{
    public static class ConfigureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ISiniestroRepository, SiniestroRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
