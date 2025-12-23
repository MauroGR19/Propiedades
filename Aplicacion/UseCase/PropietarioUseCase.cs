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
    public class PropietarioUseCase : IUseCasePropietario<Propietario, string>
    {
        #region Atributos
        private readonly IRepositorioPropietario<Propietario, string> repositorio;
        private readonly ILogger<PropietarioUseCase> _logger;
        private readonly IServicioCache _servicioCache;
        private const string claveCache = "Propietarios_todos";
        private const string claveCacheId = "Propietario_";
        #endregion

        #region Constructor
        public PropietarioUseCase(IRepositorioPropietario<Propietario, string> _repositorio, ILogger<PropietarioUseCase> logger, IServicioCache servicioCache)
        {
            repositorio = _repositorio;
            _logger = logger;
            _servicioCache = servicioCache;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(Propietario entidad)
        {
            _logger.LogInformation("Iniciando actualización de propietario {NumeroDocumento} - {Nombre}", entidad.NumeroDocumento, entidad.Nombre);
            
            Guard.NoNulo(entidad, "Propietario");
            Guard.NoNuloOVacio(entidad.NumeroDocumento, "NumeroDocumento");
            Guard.NoNuloOVacio(entidad.TipoDocumento, "TipoDocumento");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.FechaNoFutura(entidad.FechaNacimiento, "FechaNacimiento");
            Guard.FechaNoMuyAntigua(entidad.FechaNacimiento, 120, "FechaNacimiento");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidad.NumeroDocumento}");
                _logger.LogInformation("Propietario {NumeroDocumento} actualizado exitosamente", entidad.NumeroDocumento);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar propietario {NumeroDocumento}", entidad.NumeroDocumento);
                throw new ValidacionDominioException($"Error al actualizar propietario: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(string entidadID)
        {
            _logger.LogInformation("Iniciando eliminación de propietario {NumeroDocumento}", entidadID);
            
            Guard.NoNuloOVacio(entidadID, "NumeroDocumento");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("Propietario", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                await _servicioCache.RemoverAsync($"{claveCacheId}{entidadID}");
                _logger.LogInformation("Propietario {NumeroDocumento} eliminado exitosamente", entidadID);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar propietario {NumeroDocumento}", entidadID);
                throw new ValidacionDominioException($"Error al eliminar propietario: {ex.Message}");
            }
        }

        public async Task<Propietario> InsertarAsync(Propietario entidad)
        {
            _logger.LogInformation("Iniciando inserción de propietario {Nombre}", entidad.Nombre);
            
            Guard.NoNulo(entidad, "Propietario");
            Guard.NoNuloOVacio(entidad.NumeroDocumento, "NumeroDocumento");
            Guard.NoNuloOVacio(entidad.TipoDocumento, "TipoDocumento");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.FechaNoFutura(entidad.FechaNacimiento, "FechaNacimiento");
            Guard.FechaNoMuyAntigua(entidad.FechaNacimiento, 120, "FechaNacimiento");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                await _servicioCache.RemoverAsync(claveCache);
                _logger.LogInformation("Propietario insertado exitosamente con documento {NumeroDocumento} - {Nombre}", 
                    resultado.NumeroDocumento, entidad.Nombre);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar propietario {Nombre}", entidad.Nombre);
                throw new ValidacionDominioException($"Error al insertar propietario: {ex.Message}");
            }
        }

        public async Task<Propietario> ObtenerPorIDAsync(string entidadID)
        {
            _logger.LogInformation("Obteniendo propietario por documento {NumeroDocumento}", entidadID);
            
            Guard.NoNuloOVacio(entidadID, "NumeroDocumento");
            
            var claveId = $"{claveCacheId}{entidadID}";
            var enCache = await _servicioCache.ObtenerAsync<Propietario>(claveId);
            if (enCache != null)
                return enCache;
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("Propietario", entidadID);
            
            await _servicioCache.EstablecerAsync(claveId, resultado, TimeSpan.FromMinutes(5));
            _logger.LogInformation("Propietario {NumeroDocumento} obtenido exitosamente - {Nombre}", entidadID, resultado.Nombre);
            return resultado;
        }

        public async Task<List<Propietario>> ObtenerTodoAsync()
        {
            _logger.LogInformation("Obteniendo todos los propietarios");
            
            try
            {
                var enCache = await _servicioCache.ObtenerAsync<List<Propietario>>(claveCache);
                if (enCache != null)
                    return enCache;

                var resultado = await repositorio.ObtenerTodoAsync();
                await _servicioCache.EstablecerAsync(claveCache, resultado, TimeSpan.FromMinutes(10));
                _logger.LogInformation("Se obtuvieron {Cantidad} propietarios", resultado.Count);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propietarios");
                throw new ValidacionDominioException($"Error al obtener propietarios: {ex.Message}");
            }
        }

        public async Task<ResultadoPaginado<Propietario>> ObtenerPaginadoAsync(PaginacionParametros parametros)
        {
            _logger.LogInformation("Obteniendo propietarios paginados - Página: {Pagina}, Tamaño: {TamanioPagina}", 
                parametros.Pagina, parametros.TamanioPagina);
            
            Guard.NoNulo(parametros, nameof(parametros));
            Guard.MayorQue(parametros.Pagina, 0, nameof(parametros.Pagina));
            Guard.MayorQue(parametros.TamanioPagina, 0, nameof(parametros.TamanioPagina));
            Guard.MenorOIgualQue(parametros.TamanioPagina, 100, nameof(parametros.TamanioPagina));
            
            try
            {
                var resultado = await repositorio.ObtenerPaginadoAsync(parametros);
                
                _logger.LogInformation("Propietarios paginados obtenidos exitosamente - Total: {Total}, Página: {Pagina}/{TotalPaginas}", 
                    resultado.TotalRegistros, resultado.PaginaActual, resultado.TotalPaginas);
                
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propietarios paginados");
                throw new ValidacionDominioException($"Error al obtener propietarios paginados: {ex.Message}");
            }
        }
        #endregion
    }
}
