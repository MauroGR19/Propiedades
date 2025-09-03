using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;

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
        public List<Propiedad> ObtenerTodo()
        {
            return _mapper.Map<List<Propiedad>>(db.propiedadEntidad.ToList());
        }

        public List<Propiedad> ObtenerPorFiltro(Propiedad entidad, string order)
        {
            var query = db.propiedadEntidad.AsQueryable();

            if (!string.IsNullOrEmpty(entidad.Direccion))
                query = query.Where(x => x.Direccion.Contains(entidad.Direccion));

            if (entidad.Precio > 0)
                query = query.Where(x => x.Precio == entidad.Precio);

            if (entidad.Anio > 0)
                query = query.Where(x => x.Anio == entidad.Anio);

            return _mapper.Map<List<Propiedad>>(query.ToList());
        }


        public Propiedad ObtenerPorID(int entidadID)
        {
            var selecc = db.propiedadEntidad.Where(x => (x.IdPropiedad == entidadID)).FirstOrDefault();

            return _mapper.Map<Propiedad>(selecc);
        }

        public Propiedad Insertar(Propiedad entidad)
        {
            db.propiedadEntidad.Add(_mapper.Map<PropiedadEntidad>(entidad));
            return entidad;
        }

        public bool Actualizar(Propiedad entidad)
        {
            var selecc = ObtenerPorID(entidad.IdPropiedad);

            if (selecc != null)
            {
                selecc.Nombre = entidad.Nombre;
                selecc.Direccion = entidad.Direccion;
                selecc.Precio = entidad.Precio;
                selecc.Anio = entidad.Anio;
                selecc.IdPropietario = entidad.IdPropietario;

                db.Entry(_mapper.Map<PropiedadEntidad>(selecc)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                db.propiedadEntidad.Remove(_mapper.Map<PropiedadEntidad>(selecc));
                return true;
            }
            else
                return false;
        }

        public void SalvarTodo()
        {
            db.SaveChanges();
        }
        #endregion
    }
}
