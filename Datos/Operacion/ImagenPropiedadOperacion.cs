using AutoMapper;
using Datos.Contexto;
using Datos.Entidades;
using Dominio.Comun;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ImagenPropiedad> ObtenerPorIDAsync(int entidadID)
        {
            var selecc = await db.imagenEntidad.Where(x => (x.IdImagenPropiedad == entidadID)).FirstOrDefaultAsync();
            return _mapper.Map<ImagenPropiedad>(selecc);
        }

        public async Task<ImagenPropiedad> InsertarAsync(ImagenPropiedad entidad)
        {
            await db.imagenEntidad.AddAsync(_mapper.Map<ImagenPropiedadEntidad>(entidad));
            return entidad;
        }

        public async Task<bool> ActualizarAsync(ImagenPropiedad entidad)
        {
            var selecc = await ObtenerPorIDAsync(entidad.IdImagenPropiedad);
            if (selecc != null)
            {
                selecc.Archivo = entidad.Archivo;
                selecc.Habilitado = entidad.Habilitado;

                db.Entry(_mapper.Map<ImagenPropiedadEntidad>(selecc)).State = EntityState.Modified;
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
                db.imagenEntidad.Remove(_mapper.Map<ImagenPropiedadEntidad>(selecc));
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
