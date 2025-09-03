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
            .HasMany<ImagenPropiedadEntidad>(oRow => oRow.imagen)
            .WithOne(oItem => oItem.propiedad)
            .HasForeignKey(c => c.IdPropiedad);

            builder
            .HasMany<HistorialPropiedadEntidad>(oRow => oRow.historial)
            .WithOne(oItem => oItem.propiedad)
            .HasForeignKey(c => c.idPropiedad);
        }
        #endregion
    }
}
