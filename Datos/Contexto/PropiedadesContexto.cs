using Datos.Configuracion;
using Datos.Entidades;
using Dominio.Comun;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ActualizarCamposAuditoria();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ActualizarCamposAuditoria();
            return base.SaveChanges();
        }

        private void ActualizarCamposAuditoria()
        {
            var entradas = ChangeTracker.Entries<EntidadAuditable>();
            var fechaActual = DateTime.UtcNow;
            var usuario = "Sistema"; // TODO: Obtener usuario actual del contexto

            foreach (var entrada in entradas)
            {
                switch (entrada.State)
                {
                    case EntityState.Added:
                        entrada.Entity.FechaCreacion = fechaActual;
                        entrada.Entity.CreadoPor = usuario;
                        break;
                    case EntityState.Modified:
                        entrada.Entity.FechaModificacion = fechaActual;
                        entrada.Entity.ModificadoPor = usuario;
                        break;
                }
            }
        }
        #endregion
    }
}
