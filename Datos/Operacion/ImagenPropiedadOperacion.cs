using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;

namespace Datos.Operacion
{
    public class ImagenPropiedadOperacion : IRepositorioImagenPropiedad<ImagenPropiedad, int>
    {
        #region Atributos
        private PropiedadesContexto db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ImagenPropiedadOperacion(PropiedadesContexto _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        public ImagenPropiedad ObtenerPorID(int entidadID)
        {
            var selecc = db.imagenEntidad.Where(x => (x.IdImagenPropiedad == entidadID)).FirstOrDefault();

            return _mapper.Map<ImagenPropiedad>(selecc);
        }

        public ImagenPropiedad Insertar(ImagenPropiedad entidad)
        {
            db.imagenEntidad.Add(_mapper.Map<ImagenPropiedadEntidad>(entidad));
            return entidad;
        }

        public bool Actualizar(ImagenPropiedad entidad)
        {
            var selecc = ObtenerPorID(entidad.IdImagenPropiedad);
            if (selecc != null)
            {
                selecc.Archivo = entidad.Archivo;
                selecc.Habilitado = entidad.Habilitado;

                db.Entry(_mapper.Map<ImagenPropiedadEntidad>(selecc)).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                db.imagenEntidad.Remove(_mapper.Map<ImagenPropiedadEntidad>(selecc));
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
