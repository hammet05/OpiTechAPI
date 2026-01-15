using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrafficAccidentsAPI.Domain.Entities;

namespace TrafficAccidentsAPI.Infrastructure.Persistence.Configurations
{
    public class VictimaConfiguration : IEntityTypeConfiguration<Victima>
    {
        public void Configure(EntityTypeBuilder<Victima> builder)
        {
            builder.ToTable("Victimas");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id).ValueGeneratedNever();

            builder.Property(v => v.SiniestroId).IsRequired();

            builder.Property(v => v.Nombre).HasMaxLength(200).IsRequired();

            builder.Property(v => v.Edad).IsRequired();

            builder.Property(v => v.Condicion).HasMaxLength(100).IsRequired();

            builder.Property(v => v.CreatedAt).IsRequired();

            builder.Property(v => v.UpdatedAt);

            builder.HasIndex(v => v.SiniestroId).HasDatabaseName("IX_Victimas_SiniestroId");
        }
    }
}
