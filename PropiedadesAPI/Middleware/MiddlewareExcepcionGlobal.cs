using Dominio.Excepciones;
using DTO.DTO.Comun;
using System.Net;
using System.Text.Json;

namespace PropiedadesAPI.Middleware
{
    /// <summary>
    /// Middleware para el manejo centralizado de excepciones en toda la aplicación
    /// </summary>
    /// <remarks>
    /// Este middleware intercepta todas las excepciones no controladas y las convierte
    /// en respuestas HTTP apropiadas con información detallada del error.
    /// 
    /// Mapeo de excepciones a códigos HTTP:
    /// - EntidadNoEncontradaException -> 404 Not Found
    /// - ValidacionDominioException -> 400 Bad Request
    /// - AutenticacionException -> 401 Unauthorized
    /// - OperacionNoPermitidaException -> 403 Forbidden
    /// - Otras excepciones -> 500 Internal Server Error
    /// </remarks>
    public class MiddlewareExcepcionGlobal
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<MiddlewareExcepcionGlobal> _logger;

        /// <summary>
        /// Constructor del middleware de manejo de excepciones
        /// </summary>
        /// <param name="siguiente">Siguiente middleware en el pipeline</param>
        /// <param name="logger">Logger para registrar errores</param>
        public MiddlewareExcepcionGlobal(RequestDelegate siguiente, ILogger<MiddlewareExcepcionGlobal> logger)
        {
            _siguiente = siguiente;
            _logger = logger;
        }

        /// <summary>
        /// Método principal que intercepta las solicitudes HTTP y maneja las excepciones
        /// </summary>
        /// <param name="contexto">Contexto HTTP de la solicitud actual</param>
        /// <returns>Task que representa la operación asíncrona</returns>
        public async Task InvokeAsync(HttpContext contexto)
        {
            try
            {
                await _siguiente(contexto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción no controlada en {Ruta}", contexto.Request.Path);
                await ManejarExcepcionAsync(contexto, ex);
            }
        }

        /// <summary>
        /// Maneja las excepciones convirtiéndolas en respuestas HTTP apropiadas
        /// </summary>
        /// <param name="contexto">Contexto HTTP de la solicitud</param>
        /// <param name="excepcion">Excepción que se produjo</param>
        /// <returns>Task que representa la operación asíncrona</returns>
        /// <remarks>
        /// Crea un objeto DetallesError con información completa del error
        /// y lo serializa como JSON para enviar al cliente
        /// </remarks>
        private async Task ManejarExcepcionAsync(HttpContext contexto, Exception excepcion)
        {
            contexto.Response.ContentType = "application/json";

            var detallesError = new DetallesError
            {
                Ruta = contexto.Request.Path
            };

            // Mapeo de excepciones del dominio a códigos HTTP específicos
            switch (excepcion)
            {
                case EntidadNoEncontradaException noEncontrada:
                    contexto.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    detallesError.CodigoEstado = (int)HttpStatusCode.NotFound;
                    detallesError.Tipo = "EntidadNoEncontrada";
                    detallesError.Mensaje = noEncontrada.Message;
                    break;

                case ValidacionDominioException validacion:
                    contexto.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    detallesError.CodigoEstado = (int)HttpStatusCode.BadRequest;
                    detallesError.Tipo = "ValidacionDominio";
                    detallesError.Mensaje = validacion.Message;
                    break;

                case AutenticacionException autenticacion:
                    contexto.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    detallesError.CodigoEstado = (int)HttpStatusCode.Unauthorized;
                    detallesError.Tipo = "Autenticacion";
                    detallesError.Mensaje = autenticacion.Message;
                    break;

                case OperacionNoPermitidaException operacionNoPermitida:
                    contexto.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    detallesError.CodigoEstado = (int)HttpStatusCode.Forbidden;
                    detallesError.Tipo = "OperacionNoPermitida";
                    detallesError.Mensaje = operacionNoPermitida.Message;
                    break;

                default:
                    // Manejo de excepciones no esperadas del sistema
                    contexto.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    detallesError.CodigoEstado = (int)HttpStatusCode.InternalServerError;
                    detallesError.Tipo = "ErrorInterno";
                    detallesError.Mensaje = "Ocurrió un error interno en el servidor";
                    detallesError.Detalles = excepcion.Message;
                    break;
            }

            // Configuración de serialización JSON con camelCase
            var opciones = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(detallesError, opciones);
            await contexto.Response.WriteAsync(json);
        }
    }
}

