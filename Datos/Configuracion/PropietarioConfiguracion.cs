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
            builder.HasKey(x => x.IdPropietario);

            builder.Property(p => p.IdPropietario)
            .ValueGeneratedNever();

            builder
            .HasMany<PropiedadEntidad>(oRow => oRow.propiedad)
            .WithOne(oItem => oItem.Propietario)
            .HasForeignKey(c => c.idPropietario);
        }
        #endregion
    }
}
