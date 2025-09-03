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
        }
        #endregion
    }
}
