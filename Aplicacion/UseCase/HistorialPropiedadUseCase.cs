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
    public class HistorialPropiedadUseCase : IUseCaseHistorialPropiedad<HistorialPropiedad, int>
    {
        #region Atributos
        private readonly IRepositorioHistorialPropiedad<HistorialPropiedad, int> repositorio;
        private readonly ILogger<HistorialPropiedadUseCase> _logger;
        private readonly IServicioCache _servicioCache;
        private const string claveCache = "HistorialPropiedades_todas";
        private const string claveCacheId = "HistorialPropiedad_";
        #endregion

        #region Constructor
        public HistorialPropiedadUseCase(IRepositorioHistorialPropiedad<HistorialPropiedad, int> _repositorio, ILogger<HistorialPropiedadUseCase> logger, IServicioCache servicioCache)
        {
            repositorio = _repositorio;
            _logger = logger;
            _servicioCache = servicioCache;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(HistorialPropiedad entidad)
        {
            Guard.NoNulo(entidad, "HistorialPropiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.MayorQue(entidad.Valor, 0, "Valor");
            Guard.MayorOIgualQue(entidad.Impuesto, 0, "Impuesto");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");
            Guard.FechaNoFutura(entidad.FechaVenta, "FechaVenta");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidad.IdHistorialPropiedad}");
                _logger.LogInformation("Historial {HistorialPropiedad} , actualizado correctamente", entidad.IdHistorialPropiedad);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar historial: {HistorialPropiedad}", entidad.IdHistorialPropiedad);
                throw new ValidacionDominioException($"Error al actualizar historial: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            _logger.LogInformation("Iniciando eliminación de historial {Id}", entidadID);
            
            Guard.MayorQue(entidadID, 0, "IdHistorialPropiedad");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("HistorialPropiedad", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidadID}");
                
                _logger.LogInformation("Historial {Id} eliminado exitosamente", entidadID);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar historial {Id}", entidadID);
                throw new ValidacionDominioException($"Error al eliminar historial: {ex.Message}");
            }
        }

        public async Task<HistorialPropiedad> InsertarAsync(HistorialPropiedad entidad)
        {
            _logger.LogInformation("Iniciando inserción de historial para propiedad {IdPropiedad}", entidad.IdPropiedad);
            
            Guard.NoNulo(entidad, "HistorialPropiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.MayorQue(entidad.Valor, 0, "Valor");
            Guard.MayorOIgualQue(entidad.Impuesto, 0, "Impuesto");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");
            Guard.FechaNoFutura(entidad.FechaVenta, "FechaVenta");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                
                _logger.LogInformation("Historial insertado exitosamente con ID {Id} para propiedad {IdPropiedad}", 
                    resultado.IdHistorialPropiedad, entidad.IdPropiedad);
                
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar historial para propiedad {IdPropiedad}", entidad.IdPropiedad);
                throw new ValidacionDominioException($"Error al insertar historial: {ex.Message}");
            }
        }

        public async Task<HistorialPropiedad> ObtenerPorIDAsync(int entidadID)
        {
            _logger.LogInformation("Obteniendo historial por ID {Id}", entidadID);
            
            Guard.MayorQue(entidadID, 0, "IdHistorialPropiedad");
            
            var claveId = $"{claveCacheId}{entidadID}";
            var enCache = await _servicioCache.ObtenerAsync<HistorialPropiedad>(claveId);
            if (enCache != null)
                return enCache;
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("HistorialPropiedad", entidadID);
            
            await _servicioCache.EstablecerAsync(claveId, resultado, TimeSpan.FromMinutes(5));
            _logger.LogInformation("Historial {Id} obtenido exitosamente", entidadID);
            return resultado;
        }

        public async Task<List<HistorialPropiedad>> ObtenerTodoAsync()
        {
            _logger.LogInformation("Obteniendo todos los historiales");
            
            try
            {
                var enCache = await _servicioCache.ObtenerAsync<List<HistorialPropiedad>>(claveCache);
                if (enCache != null)
                    return enCache;

                var resultado = await repositorio.ObtenerTodoAsync();
                await _servicioCache.EstablecerAsync(claveCache, resultado, TimeSpan.FromMinutes(10));
                _logger.LogInformation("Se obtuvieron {Cantidad} historiales", resultado.Count);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales");
                throw new ValidacionDominioException($"Error al obtener historiales: {ex.Message}");
            }
        }

        public async Task<ResultadoPaginado<HistorialPropiedad>> ObtenerPaginadoAsync(PaginacionParametros parametros)
        {
            _logger.LogInformation("Obteniendo historiales paginados - Página: {Pagina}, Tamaño: {TamanioPagina}", 
                parametros.Pagina, parametros.TamanioPagina);
            
            Guard.NoNulo(parametros, nameof(parametros));
            Guard.MayorQue(parametros.Pagina, 0, nameof(parametros.Pagina));
            Guard.MayorQue(parametros.TamanioPagina, 0, nameof(parametros.TamanioPagina));
            Guard.MenorOIgualQue(parametros.TamanioPagina, 100, nameof(parametros.TamanioPagina));
            
            try
            {
                var resultado = await repositorio.ObtenerPaginadoAsync(parametros);
                
                _logger.LogInformation("Historiales paginados obtenidos exitosamente - Total: {Total}, Página: {Pagina}/{TotalPaginas}", 
                    resultado.TotalRegistros, resultado.PaginaActual, resultado.TotalPaginas);
                
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener historiales paginados");
                throw new ValidacionDominioException($"Error al obtener historiales paginados: {ex.Message}");
            }
        }
        #endregion

    }
}
