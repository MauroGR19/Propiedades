using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using Dominio.Excepciones;
using Dominio.Comun;
using Microsoft.Extensions.Logging;

namespace Aplicacion.UseCase
{

    public class PropiedadesUseCase : IUseCasePropiedad<Propiedad, int>
    {
        #region Atributos
        private readonly IRepositorioPropiedad<Propiedad, int> repositorio;
        private readonly ILogger<PropiedadesUseCase> _logger;
        #endregion

        #region Constructor
        public PropiedadesUseCase(IRepositorioPropiedad<Propiedad, int> _repositorio, ILogger<PropiedadesUseCase> logger)
        {
            repositorio = _repositorio;
            _logger = logger;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(Propiedad entidad)
        {
            _logger.LogInformation("Iniciando actualización de propiedad {Id} - {Nombre}", entidad.IdPropiedad, entidad.Nombre);
            
            Guard.NoNulo(entidad, "Propiedad");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.MayorQue(entidad.Precio, 0, "Precio");
            Guard.NoNuloOVacio(entidad.CodigoInterno, "CodigoInterno");
            Guard.AnioValido(entidad.Anio, "Anio");
            Guard.MayorQue(entidad.IdPropietario, 0, "IdPropietario");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                
                _logger.LogInformation("Propiedad {Id} actualizada exitosamente", entidad.IdPropiedad);
                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar propiedad {Id}", entidad.IdPropiedad);
                throw new ValidacionDominioException($"Error al actualizar propiedad: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            _logger.LogInformation("Iniciando eliminación de propiedad {Id}", entidadID);
            
            Guard.MayorQue(entidadID, 0, "IdPropiedad");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("Propiedad", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                
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
            Guard.MayorQue(entidad.IdPropietario, 0, "IdPropietario");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                
                _logger.LogInformation("Propiedad insertada exitosamente con ID {Id} - {Nombre}", 
                    resultado.IdPropiedad, entidad.Nombre);
                
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

        public async Task<Propiedad> ObtenerPorIDAsync(int entidadID)
        {
            _logger.LogInformation("Obteniendo propiedad por ID {Id}", entidadID);
            
            Guard.MayorQue(entidadID, 0, "IdPropiedad");
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("Propiedad", entidadID);
            
            _logger.LogInformation("Propiedad {Id} obtenida exitosamente - {Nombre}", entidadID, resultado.Nombre);
            return resultado;
        }

        public async Task<List<Propiedad>> ObtenerTodoAsync()
        {
            _logger.LogInformation("Obteniendo todas las propiedades");
            
            try
            {
                var resultado = await repositorio.ObtenerTodoAsync();
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
