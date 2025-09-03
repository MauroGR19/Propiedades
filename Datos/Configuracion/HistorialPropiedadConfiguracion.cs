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
            .ValueGeneratedNever();
        }
        #endregion
    }
}
