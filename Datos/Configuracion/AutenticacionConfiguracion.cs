using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Datos.Configuracion
{
    public class AutenticacionConfiguracion : IEntityTypeConfiguration<AutenticacionEntidad>
    {
        #region Metodos
        public void Configure(EntityTypeBuilder<AutenticacionEntidad> builder)
        {
            builder.ToTable(nameof(AutenticacionEntidad));
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd();

            builder.HasOne(a => a.PropietarioEntidad)
           .WithOne(p => p.Autenticacion)
           .HasForeignKey<AutenticacionEntidad>(a => a.NumeroDocumentoPropietario)
           .IsRequired(false);

        }
        #endregion
    }
}
