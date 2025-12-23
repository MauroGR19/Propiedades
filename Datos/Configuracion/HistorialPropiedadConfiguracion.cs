using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Configuracion
{
    public class HistorialPropiedadConfiguracion : IEntityTypeConfiguration<HistorialPropiedadEntidad>
    {
        #region Metodos
        public void Configure(EntityTypeBuilder<HistorialPropiedadEntidad> builder)
        {
            builder.ToTable(nameof(HistorialPropiedadEntidad));
            builder.HasKey(x => x.IdHistorialPropiedad);

            builder.Property(p => p.IdHistorialPropiedad)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.MatriculaInmobiliaria)
                .IsRequired()
                .HasMaxLength(30);

            // Relación con Propiedad
            builder.HasOne(h => h.Propiedad)
                .WithMany(p => p.Historial)
                .HasForeignKey(h => h.MatriculaInmobiliaria)
                .HasPrincipalKey(p => p.MatriculaInmobiliaria)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de campos de auditoría
            builder.Property(p => p.FechaCreacion)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
            
            builder.Property(p => p.CreadoPor)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("Sistema");
            
            builder.Property(p => p.FechaModificacion)
                .IsRequired(false);
            
            builder.Property(p => p.ModificadoPor)
                .IsRequired(false)
                .HasMaxLength(100);
        }
        #endregion
    }
}
