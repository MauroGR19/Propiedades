using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Datos.Operacion
{
    public class PropietarioOperacion : IRepositorioBase<Propietario, int>
    {
        #region Atributos
        private PropiedadesContexto db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PropietarioOperacion(PropiedadesContexto _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        public async Task<Propietario> InsertarAsync(Propietario entidad)
        {
            await db.propietarioEntidad.AddAsync(_mapper.Map<PropietarioEntidad>(entidad));
            return entidad;
        }

        public async Task<bool> ActualizarAsync(Propietario entidad)
        {
            var selecc = await ObtenerPorIDAsync(entidad.IdPropietario);
            if (selecc != null)
            {
                selecc.Nombre = entidad.Nombre;
                selecc.Direccion = entidad.Direccion;
                selecc.Foto = entidad.Foto;
                selecc.FechaNacimiento = entidad.FechaNacimiento;

                db.Entry(_mapper.Map<PropietarioEntidad>(selecc)).State = EntityState.Modified;
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
                db.propietarioEntidad.Remove(_mapper.Map<PropietarioEntidad>(selecc));
                return true;
            }
            else
                return false;
        }

        public async Task<List<Propietario>> ObtenerTodoAsync()
        {
            var entidades = await db.propietarioEntidad.ToListAsync();
            return _mapper.Map<List<Propietario>>(entidades);
        }

        public async Task<Propietario> ObtenerPorIDAsync(int entidadID)
        {
            var selecc = await db.propietarioEntidad.Where(x => (x.IdPropietario == entidadID)).FirstOrDefaultAsync();
            return _mapper.Map<Propietario>(selecc);
        }

        public async Task SalvarTodoAsync()
        {
            await db.SaveChangesAsync();
        }
        #endregion
    }
}
