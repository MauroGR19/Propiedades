using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;

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
        public Propietario Insertar(Propietario entidad)
        {
            db.propietarioEntidad.Add(_mapper.Map<PropietarioEntidad>(entidad));
            return entidad;
        }

        public bool Actualizar(Propietario entidad)
        {
            var selecc = ObtenerPorID(entidad.IdPropietario);
            if (selecc != null)
            {
                selecc.Nombre = entidad.Nombre;
                selecc.Direccion = entidad.Direccion;
                selecc.Foto = entidad.Foto;
                selecc.FechaNacimiento = entidad.FechaNacimiento;

                db.Entry(_mapper.Map<PropietarioEntidad>(selecc)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return true;
            }
            else
                return false;
        }

        public bool Eliminar(int entidadID)
        {
            var selecc = ObtenerPorID(entidadID);
            if (selecc != null)
            {
                db.propietarioEntidad.Remove(_mapper.Map<PropietarioEntidad>(selecc));
                return true;
            }
            else
                return false;
        }

        public List<Propietario> ObtenerTodo()
        {
            return _mapper.Map<List<Propietario>>(db.propietarioEntidad.ToList());
        }

        public Propietario ObtenerPorID(int entidadID)
        {
            var selecc = db.propietarioEntidad.Where(x => (x.IdPropietario == entidadID)).FirstOrDefault();

            return _mapper.Map<Propietario>(selecc);
        }

        public void SalvarTodo()
        {
            db.SaveChanges();
        }
        #endregion
    }
}
