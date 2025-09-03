using Datos.Configuracion;
using Datos.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Datos.Contexto
{
    public class PropiedadesContexto : DbContext
    {
        #region Atributos
        public DbSet<AutenticacionEntidad> autenticacionEntidad { get; set; }
        public DbSet<HistorialPropiedadEntidad> historialPropiedadEntidad { get; set; }
        public DbSet<ImagenPropiedadEntidad> imagenEntidad { get; set; }
        public DbSet<PropiedadEntidad> propiedadEntidad { get; set; }
        public DbSet<PropietarioEntidad> propietarioEntidad { get; set; }
        #endregion

        #region Constructor
        public PropiedadesContexto(DbContextOptions<PropiedadesContexto> options)
                : base(options)
        {
        }
        #endregion

        #region Metodos
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AutenticacionConfiguracion());
            builder.ApplyConfiguration(new HistorialPropiedadConfiguracion());
            builder.ApplyConfiguration(new ImagenPropiedadConfiguracion());
            builder.ApplyConfiguration(new PropiedadConfiguraion());
            builder.ApplyConfiguration(new PropietarioConfiguracion());

        }
        #endregion
    }
}
