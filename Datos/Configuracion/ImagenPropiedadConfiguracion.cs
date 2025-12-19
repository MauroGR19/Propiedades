using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Configuracion
{
    public class ImagenPropiedadConfiguracion : IEntityTypeConfiguration<ImagenPropiedadEntidad>
    {
        #region Metodos
        public void Configure(EntityTypeBuilder<ImagenPropiedadEntidad> builder)
        {
            builder.ToTable(nameof(ImagenPropiedadEntidad));
            builder.HasKey(x => x.IdImagenPropiedad);

            builder.Property(p => p.IdImagenPropiedad)
            .ValueGeneratedNever();

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
