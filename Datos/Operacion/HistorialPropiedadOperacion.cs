using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;

namespace Datos.Operacion
{
    public class HistorialPropiedadOperacion : IRepositorioBase<HistorialPropiedad, int>
    {
        #region Atributos
        private PropiedadesContexto db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public HistorialPropiedadOperacion(PropiedadesContexto _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        public HistorialPropiedad Insertar(HistorialPropiedad entidad)
        {
            db.historialPropiedadEntidad.Add(_mapper.Map<HistorialPropiedadEntidad>(entidad));
            return entidad;
        }

        public bool Actualizar(HistorialPropiedad entidad)
        {
            var selecc = ObtenerPorID(entidad.IdHistorialPropiedad);
            if (selecc != null)
            {
                selecc.FechaVenta = entidad.FechaVenta;
                selecc.Nombre = entidad.Nombre;
                selecc.Valor = entidad.Valor;
                selecc.Impuesto = entidad.Impuesto;

                db.Entry(_mapper.Map<HistorialPropiedadEntidad>(selecc)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                db.historialPropiedadEntidad.Remove(_mapper.Map<HistorialPropiedadEntidad>(selecc));
                return true;
            }
            else
                return false;
        }

        public List<HistorialPropiedad> ObtenerTodo()
        {
            return _mapper.Map<List<HistorialPropiedad>>(db.historialPropiedadEntidad.ToList());
        }

        public HistorialPropiedad ObtenerPorID(int entidadID)
        {
            var selecc = db.historialPropiedadEntidad.Where(x => (x.IdHistorialPropiedad == entidadID)).FirstOrDefault();

            return _mapper.Map<HistorialPropiedad>(selecc);
        }

        public void SalvarTodo()
        {
            db.SaveChanges();
        }
        #endregion
    }
}
