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
