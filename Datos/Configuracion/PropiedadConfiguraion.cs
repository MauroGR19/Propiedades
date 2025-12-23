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
            builder.HasKey(x => x.MatriculaInmobiliaria);

            builder.Property(p => p.MatriculaInmobiliaria)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(p => p.CodigoInterno)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.NumeroDocumentoPropietario)
                .IsRequired()
                .HasMaxLength(20);

            // Relación con Propietario
            builder.HasOne(p => p.Propietario)
                .WithMany(pr => pr.Propiedad)
                .HasForeignKey(p => p.NumeroDocumentoPropietario)
                .HasPrincipalKey(pr => pr.NumeroDocumento)
                .OnDelete(DeleteBehavior.Restrict);

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
            .HasForeignKey(c => c.MatriculaInmobiliaria);

            builder
            .HasMany<HistorialPropiedadEntidad>(oRow => oRow.Historial)
            .WithOne(oItem => oItem.Propiedad)
            .HasForeignKey(c => c.MatriculaInmobiliaria);
        }
        #endregion
    }
}
