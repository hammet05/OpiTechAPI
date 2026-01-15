using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrafficAccidentsAPI.Domain.Entities;

namespace TrafficAccidentsAPI.Infrastructure.Persistence.Configurations
{
    public class SiniestroConfiguration : IEntityTypeConfiguration<Siniestro>
    {
        public void Configure(EntityTypeBuilder<Siniestro> builder)
        {
            builder.ToTable("Siniestros");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id).ValueGeneratedNever();


            builder.OwnsOne(s => s.FechaHora, fh =>
            {
                fh.Property(f => f.Valor)
                    .HasColumnName("FechaHora")
                    .IsRequired();

                fh.HasIndex(f => f.Valor)
                    .HasDatabaseName("IX_Siniestros_FechaHora");
            });


            builder.OwnsOne(s => s.Ubicacion, ub =>
            {
                ub.Property(u => u.Departamento)
                    .HasColumnName("Departamento")
                    .HasMaxLength(100)
                    .IsRequired();

                ub.Property(u => u.Ciudad)
                    .HasColumnName("Ciudad")
                    .HasMaxLength(100)
                    .IsRequired();

                ub.HasIndex(u => u.Departamento)
                    .HasDatabaseName("IX_Siniestros_Departamento");
            });

            builder.Property(s => s.Tipo).HasConversion<int>().IsRequired();

            builder.Property(s => s.Descripcion).HasMaxLength(1000);

            builder.Property(s => s.CreatedAt).IsRequired();

            builder.Property(s => s.UpdatedAt);


            builder.HasMany(s => s.Vehiculos).WithOne().HasForeignKey(v => v.SiniestroId).OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(s => s.Victimas).WithOne().HasForeignKey(v => v.SiniestroId).OnDelete(DeleteBehavior.Cascade);


            builder.Ignore(s => s.NumeroVictimas);
        }
    }
}
