using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrafficAccidentsAPI.Domain.Entities;

namespace TrafficAccidentsAPI.Infrastructure.Persistence.Configurations
{
    public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
    {
        public void Configure(EntityTypeBuilder<Vehiculo> builder)
        {
            builder.ToTable("Vehiculos");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id).ValueGeneratedNever();

            builder.Property(v => v.SiniestroId).IsRequired();

            builder.Property(v => v.Tipo).HasConversion<int>().IsRequired();

            builder.Property(v => v.Placa).HasMaxLength(20);

            builder.Property(v => v.CreatedAt).IsRequired();

            builder.Property(v => v.UpdatedAt);

            builder.HasIndex(v => v.SiniestroId).HasDatabaseName("IX_Vehiculos_SiniestroId");
        }
    }
}
