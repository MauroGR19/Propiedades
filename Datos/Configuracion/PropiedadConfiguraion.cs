using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Configuracion
{
    public class PropiedadConfiguraion : IEntityTypeConfiguration<PropiedadEntidad>
    {
        #region Metodos
        public void Configure(EntityTypeBuilder<PropiedadEntidad> builder)
        {
            builder.ToTable(nameof(PropiedadEntidad));
            builder.HasKey(x => x.IdPropiedad);

            builder.Property(p => p.IdPropiedad)
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

            builder
            .HasMany<ImagenPropiedadEntidad>(oRow => oRow.Imagen)
            .WithOne(oItem => oItem.Propiedad)
            .HasForeignKey(c => c.IdPropiedad);

            builder
            .HasMany<HistorialPropiedadEntidad>(oRow => oRow.Historial)
            .WithOne(oItem => oItem.Propiedad)
            .HasForeignKey(c => c.IdPropiedad);
        }
        #endregion
    }
}
