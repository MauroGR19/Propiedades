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

    public class PropiedadesUseCase : IUseCasePropiedad<Propiedad, string>
    {
        #region Atributos
        private readonly IRepositorioPropiedad<Propiedad, string> repositorio;
        private readonly ILogger<PropiedadesUseCase> _logger;
        private readonly IServicioCache _servicioCache;
        private const string claveCache = "Propiedades_todas";
        private const string claveCacheId = "Propiedad_";
        #endregion

        #region Constructor
        public PropiedadesUseCase(IRepositorioPropiedad<Propiedad, string> _repositorio, ILogger<PropiedadesUseCase> logger, IServicioCache servicioCache)
        {
            repositorio = _repositorio;
            _logger = logger;
            _servicioCache = servicioCache;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(Propiedad entidad)
        {
            _logger.LogInformation("Iniciando actualización de propiedad {Id} - {Nombre}", entidad.MatriculaInmobiliaria, entidad.Nombre);
            
            Guard.NoNulo(entidad, "Propiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.MayorQue(entidad.Precio, 0, "Precio");
            Guard.NoNuloOVacio(entidad.CodigoInterno, "CodigoInterno");
            Guard.AnioValido(entidad.Anio, "Anio");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidad.MatriculaInmobiliaria}");
                _logger.LogInformation("Propiedad {Id} actualizada exitosamente", entidad.MatriculaInmobiliaria);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar propiedad {Id}", entidad.MatriculaInmobiliaria);
                throw new ValidacionDominioException($"Error al actualizar propiedad: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(string entidadID)
        {
            _logger.LogInformation("Iniciando eliminación de propiedad {Id}", entidadID);
            
            Guard.NoNuloOVacio(entidadID, "MatriculaInmobiliaria");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("Propiedad", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidadID}");
                _logger.LogInformation("Propiedad {Id} eliminada exitosamente", entidadID);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar propiedad {Id}", entidadID);
                throw new ValidacionDominioException($"Error al eliminar propiedad: {ex.Message}");
            }
        }

        public async Task<Propiedad> InsertarAsync(Propiedad entidad)
        {
            _logger.LogInformation("Iniciando inserción de propiedad {Nombre} - Precio: {Precio}", entidad.Nombre, entidad.Precio);
            
            Guard.NoNulo(entidad, "Propiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.MayorQue(entidad.Precio, 0, "Precio");
            Guard.NoNuloOVacio(entidad.CodigoInterno, "CodigoInterno");
            Guard.AnioValido(entidad.Anio, "Anio");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache); 
                _logger.LogInformation("Propiedad insertada exitosamente con ID {Id} - {Nombre}", 
                    resultado.MatriculaInmobiliaria, entidad.Nombre);
                
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar propiedad {Nombre}", entidad.Nombre);
                throw new ValidacionDominioException($"Error al insertar propiedad: {ex.Message}");
            }
        }

        public async Task<List<Propiedad>> ObtenerPorFiltroAsync(Propiedad entidad, string order)
        {
            _logger.LogInformation("Obteniendo propiedades por filtro - Orden: {Order}", order);
            
            Guard.NoNulo(entidad, "Propiedad");
            Guard.NoNuloOVacio(order, "Order");
            
            var ordenesValidas = new[] { "ASC", "DESC" };
            if (!ordenesValidas.Contains(order.ToUpper()))
                throw new ValidacionDominioException("Order", order, "debe ser ASC o DESC");

            try
            {
                var resultado = await repositorio.ObtenerPorFiltroAsync(entidad, order);
                _logger.LogInformation("Se obtuvieron {Cantidad} propiedades con filtro", resultado.Count);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propiedades por filtro");
                throw new ValidacionDominioException($"Error al obtener propiedades por filtro: {ex.Message}");
            }
        }

        public async Task<Propiedad> ObtenerPorIDAsync(string entidadID)
        {
            _logger.LogInformation("Obteniendo propiedad por ID {Id}", entidadID);
            
            Guard.NoNuloOVacio(entidadID, "MatriculaInmobiliaria");
            
            var claveId = $"{claveCacheId}{entidadID}";
            var enCache = await _servicioCache.ObtenerAsync<Propiedad>(claveId);
            if (enCache != null)
                return enCache;
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("Propiedad", entidadID);
            
            await _servicioCache.EstablecerAsync(claveId, resultado, TimeSpan.FromMinutes(5));
            _logger.LogInformation("Propiedad {Id} obtenida exitosamente - {Nombre}", entidadID, resultado.Nombre);
            return resultado;
        }

        public async Task<List<Propiedad>> ObtenerTodoAsync()
        {
            _logger.LogInformation("Obteniendo todas las propiedades");
            
            try
            {
                var enCache = await _servicioCache.ObtenerAsync<List<Propiedad>>(claveCache);
                if (enCache != null)
                    return enCache;

                var resultado = await repositorio.ObtenerTodoAsync();
                await _servicioCache.EstablecerAsync(claveCache, resultado, TimeSpan.FromMinutes(10));
                _logger.LogInformation("Se obtuvieron {Cantidad} propiedades", resultado.Count);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propiedades");
                throw new ValidacionDominioException($"Error al obtener propiedades: {ex.Message}");
            }
        }

        public async Task<ResultadoPaginado<Propiedad>> ObtenerPaginadoAsync(PaginacionParametros parametros)
        {
            _logger.LogInformation("Obteniendo propiedades paginadas - Página: {Pagina}, Tamaño: {TamanioPagina}", 
                parametros.Pagina, parametros.TamanioPagina);
            
            Guard.NoNulo(parametros, nameof(parametros));
            Guard.MayorQue(parametros.Pagina, 0, nameof(parametros.Pagina));
            Guard.MayorQue(parametros.TamanioPagina, 0, nameof(parametros.TamanioPagina));
            Guard.MenorOIgualQue(parametros.TamanioPagina, 100, nameof(parametros.TamanioPagina));
            
            try
            {
                var resultado = await repositorio.ObtenerPaginadoAsync(parametros);
                
                _logger.LogInformation("Propiedades paginadas obtenidas exitosamente - Total: {Total}, Página: {Pagina}/{TotalPaginas}", 
                    resultado.TotalRegistros, resultado.PaginaActual, resultado.TotalPaginas);
                
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propiedades paginadas");
                throw new ValidacionDominioException($"Error al obtener propiedades paginadas: {ex.Message}");
            }
        }
        #endregion
    }
}
