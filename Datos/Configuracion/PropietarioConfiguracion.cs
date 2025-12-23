using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Configuracion
{
    public class PropietarioConfiguracion : IEntityTypeConfiguration<PropietarioEntidad>
    {
        #region Metodos
        public void Configure(EntityTypeBuilder<PropietarioEntidad> builder)
        {
            builder.ToTable(nameof(PropietarioEntidad));
            builder.HasKey(x => x.NumeroDocumento);

            builder.Property(p => p.NumeroDocumento)
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
            .HasMany<PropiedadEntidad>(oRow => oRow.Propiedad)
            .WithOne(oItem => oItem.Propietario)
            .HasForeignKey(c => c.NumeroDocumentoPropietario);
        }
        #endregion
    }
}
