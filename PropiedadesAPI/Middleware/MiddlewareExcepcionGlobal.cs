using Dominio.Excepciones;
using DTO.DTO.Comun;
using System.Net;
using System.Text.Json;

namespace PropiedadesAPI.Middleware
{
    public class MiddlewareExcepcionGlobal
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<MiddlewareExcepcionGlobal> _logger;

        public MiddlewareExcepcionGlobal(RequestDelegate siguiente, ILogger<MiddlewareExcepcionGlobal> logger)
        {
            _siguiente = siguiente;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            try
            {
                await _siguiente(contexto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción no controlada");
                await ManejarExcepcionAsync(contexto, ex);
            }
        }

        private async Task ManejarExcepcionAsync(HttpContext contexto, Exception excepcion)
        {
            contexto.Response.ContentType = "application/json";

            var detallesError = new DetallesError
            {
                Ruta = contexto.Request.Path
            };

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
                    contexto.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    detallesError.CodigoEstado = (int)HttpStatusCode.InternalServerError;
                    detallesError.Tipo = "ErrorInterno";
                    detallesError.Mensaje = "Ocurrió un error interno en el servidor";
                    detallesError.Detalles = excepcion.Message;
                    break;
            }

            var opciones = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(detallesError, opciones);
            await contexto.Response.WriteAsync(json);
        }
    }
}

