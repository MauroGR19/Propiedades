using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using static Dominio.Maestras.MensajesBase;
using Dominio.Excepciones;
using Dominio.Comun;

namespace Aplicacion.UseCase
{
    public class ImagenPropiedadUseCase : IUseCaseImagenPropiedad<ImagenPropiedad, int>
    {
        #region Atributos
        private readonly IRepositorioImagenPropiedad<ImagenPropiedad, int> repositorio;
        #endregion

        #region Constructor
        public ImagenPropiedadUseCase(IRepositorioImagenPropiedad<ImagenPropiedad, int> _repositorio)
        {
            repositorio = _repositorio;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(ImagenPropiedad entidad)
        {
            Guard.NoNulo(entidad, "ImagenPropiedad");
            Guard.ArchivoImagenValido(entidad.Archivo, "Archivo");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");
            Guard.MayorQue(entidad.IdImagenPropiedad, 0, "IdImagenPropiedad");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al actualizar imagen: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdImagenPropiedad");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("ImagenPropiedad", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al eliminar imagen: {ex.Message}");
            }
        }

        public async Task<ImagenPropiedad> InsertarAsync(ImagenPropiedad entidad)
        {
            Guard.NoNulo(entidad, "ImagenPropiedad");
            Guard.ArchivoImagenValido(entidad.Archivo, "Archivo");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al insertar imagen: {ex.Message}");
            }
        }

        public async Task<ImagenPropiedad> ObtenerPorIDAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdImagenPropiedad");
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("ImagenPropiedad", entidadID);
                
            return resultado;
        }
        #endregion
    }
}
