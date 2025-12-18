using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Datos.Operacion
{
    public class PropiedadOperacion : IRepositorioPropiedad<Propiedad, int>
    {
        #region Atributos
        private PropiedadesContexto db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PropiedadOperacion(PropiedadesContexto _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        public async Task<List<Propiedad>> ObtenerTodoAsync()
        {
            var entidades = await db.propiedadEntidad.ToListAsync();
            return _mapper.Map<List<Propiedad>>(entidades);
        }

        public async Task<List<Propiedad>> ObtenerPorFiltroAsync(Propiedad entidad, string order)
        {
            var query = db.propiedadEntidad.AsQueryable();

            if (!string.IsNullOrEmpty(entidad.Direccion))
                query = query.Where(x => x.Direccion.Contains(entidad.Direccion));

            if (entidad.Precio > 0)
                query = query.Where(x => x.Precio == entidad.Precio);

            if (entidad.Anio > 0)
                query = query.Where(x => x.Anio == entidad.Anio);

            var entidades = await query.ToListAsync();
            return _mapper.Map<List<Propiedad>>(entidades);
        }


        public async Task<Propiedad> ObtenerPorIDAsync(int entidadID)
        {
            var selecc = await db.propiedadEntidad.Where(x => (x.IdPropiedad == entidadID)).FirstOrDefaultAsync();
            return _mapper.Map<Propiedad>(selecc);
        }

        public async Task<Propiedad> InsertarAsync(Propiedad entidad)
        {
            await db.propiedadEntidad.AddAsync(_mapper.Map<PropiedadEntidad>(entidad));
            return entidad;
        }

        public async Task<bool> ActualizarAsync(Propiedad entidad)
        {
            var selecc = await ObtenerPorIDAsync(entidad.IdPropiedad);
            if (selecc != null)
            {
                selecc.Nombre = entidad.Nombre;
                selecc.Direccion = entidad.Direccion;
                selecc.Precio = entidad.Precio;
                selecc.Anio = entidad.Anio;
                selecc.IdPropietario = entidad.IdPropietario;

                db.Entry(_mapper.Map<PropiedadEntidad>(selecc)).State = EntityState.Modified;
                return true;
            }
            else
                return false;
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            var selecc = await ObtenerPorIDAsync(entidadID);
            if (selecc != null)
            {
                db.propiedadEntidad.Remove(_mapper.Map<PropiedadEntidad>(selecc));
                return true;
            }
            else
                return false;
        }

        public async Task SalvarTodoAsync()
        {
            await db.SaveChangesAsync();
        }
        #endregion
    }
}
