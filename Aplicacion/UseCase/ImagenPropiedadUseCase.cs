using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using Dominio.Excepciones;
using Dominio.Comun;
using Microsoft.Extensions.Logging;
using Aplicacion.Servicios.Interfaces;

namespace Aplicacion.UseCase
{
    public class ImagenPropiedadUseCase : IUseCaseImagenPropiedad<ImagenPropiedad, int>
    {
        #region Atributos
        private readonly IRepositorioImagenPropiedad<ImagenPropiedad, int> repositorio;
        private readonly ILogger<ImagenPropiedadUseCase> _logger;
        private readonly IServicioCache _servicioCache;
        private const string claveCache = "ImagenesPropiedades_todas";
        private const string claveCacheId = "ImagenPropiedad_";
        #endregion

        #region Constructor
        public ImagenPropiedadUseCase(IRepositorioImagenPropiedad<ImagenPropiedad, int> _repositorio, ILogger<ImagenPropiedadUseCase> logger, IServicioCache servicioCache)
        {
            repositorio = _repositorio;
            _logger = logger;
            _servicioCache = servicioCache;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(ImagenPropiedad entidad)
        {
            _logger.LogInformation("Iniciando actualización de imagen {Id} para propiedad {IdPropiedad}", entidad.IdImagenPropiedad, entidad.MatriculaInmobiliaria);
            
            Guard.NoNulo(entidad, "ImagenPropiedad");
            Guard.ArchivoImagenValido(entidad.Archivo, "Archivo");
            Guard.MayorQue(entidad.IdImagenPropiedad, 0, "IdImagenPropiedad");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidad.IdImagenPropiedad}");
                
                _logger.LogInformation("Imagen {Id} actualizada exitosamente", entidad.IdImagenPropiedad);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar imagen {Id}", entidad.IdImagenPropiedad);
                throw new ValidacionDominioException($"Error al actualizar imagen: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            _logger.LogInformation("Iniciando eliminación de imagen {Id}", entidadID);
            
            Guard.MayorQue(entidadID, 0, "IdImagenPropiedad");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("ImagenPropiedad", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidadID}");
                
                _logger.LogInformation("Imagen {Id} eliminada exitosamente", entidadID);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar imagen {Id}", entidadID);
                throw new ValidacionDominioException($"Error al eliminar imagen: {ex.Message}");
            }
        }

        public async Task<ImagenPropiedad> InsertarAsync(ImagenPropiedad entidad)
        {
            _logger.LogInformation("Iniciando inserción de imagen {Archivo} para propiedad {IdPropiedad}", entidad.Archivo, entidad.MatriculaInmobiliaria);
            
            Guard.NoNulo(entidad, "ImagenPropiedad");
            Guard.ArchivoImagenValido(entidad.Archivo, "Archivo");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                
                _logger.LogInformation("Imagen insertada exitosamente con ID {Id} para propiedad {IdPropiedad}", 
                    resultado.IdImagenPropiedad, entidad.MatriculaInmobiliaria);
                
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar imagen para propiedad {IdPropiedad}", entidad.MatriculaInmobiliaria);
                throw new ValidacionDominioException($"Error al insertar imagen: {ex.Message}");
            }
        }

        public async Task<ImagenPropiedad> ObtenerPorIDAsync(int entidadID)
        {
            _logger.LogInformation("Obteniendo imagen por ID {Id}", entidadID);
            
            Guard.MayorQue(entidadID, 0, "IdImagenPropiedad");
            
            var claveId = $"{claveCacheId}{entidadID}";
            var enCache = await _servicioCache.ObtenerAsync<ImagenPropiedad>(claveId);
            if (enCache != null)
                return enCache;
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("ImagenPropiedad", entidadID);
            
            await _servicioCache.EstablecerAsync(claveId, resultado, TimeSpan.FromMinutes(5));
            _logger.LogInformation("Imagen {Id} obtenida exitosamente", entidadID);
            return resultado;
        }


        #endregion
    }
}
