using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.Modelos;
using DTO.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DTO.DTO.Comun;

namespace PropiedadesAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar la autenticación de usuarios
    /// </summary>
    /// <remarks>
    /// Para usar el token en Swagger: Bearer {token}
    /// Para usar en Postman: Tipo Bearer Token
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        #region Atributos
        private readonly IUseCaseAutenticacion<Autenticacion, string> db;
        private readonly IMapper _mapper;
        private readonly ILogger<AutenticacionController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del controlador de autenticación
        /// </summary>
        /// <param name="_db">Caso de uso para autenticación</param>
        /// <param name="mapper">Mapper para conversión de DTOs</param>
        /// <param name="logger">Logger para registro de eventos</param>
        public AutenticacionController(IUseCaseAutenticacion<Autenticacion, string> _db, IMapper mapper, ILogger<AutenticacionController> logger)
        {
            db = _db;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Registra un nuevo usuario en el sistema
        /// </summary>
        /// <param name="entidad">Datos del usuario a registrar (usuario y contraseña)</param>
        /// <returns>Confirmación de registro exitoso</returns>
        /// <response code="200">Usuario registrado exitosamente</response>
        /// <response code="400">Datos inválidos o usuario ya existe</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [Route("Registrar")]
        [ProducesResponseType(typeof(RespuestaApi<object>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RespuestaApi<object>>> Insertar([FromBody] AutenticacionDTO entidad)
        {
            _logger.LogInformation("Solicitud HTTP POST recibida para crear usuario {Usuario}", entidad?.Usuario);
            
            await db.InsertarAsync(_mapper.Map<Autenticacion>(entidad));
            
            _logger.LogInformation("Usuario {Usuario} creado exitosamente", entidad.Usuario);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Insertado("Usuario")));
        }

        /// <summary>
        /// Autentica un usuario y genera un token JWT
        /// </summary>
        /// <param name="autenticacion">Credenciales del usuario (usuario y contraseña)</param>
        /// <returns>Token JWT válido por 1 hora si la autenticación es exitosa</returns>
        /// <response code="200">Autenticación exitosa, retorna token JWT</response>
        /// <response code="401">Credenciales inválidas</response>
        /// <response code="400">Datos de entrada inválidos</response>
        /// <response code="500">Error interno del servidor</response>
        /// <example>
        /// POST /api/Autenticacion/Validar
        /// {
        ///   "usuario": "admin",
        ///   "contrasena": "password123"
        /// }
        /// </example>
        [HttpPost]
        [Route("Validar")]
        [ProducesResponseType(typeof(RespuestaApi<object>), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<RespuestaApi<object>>> ObtenerAutenticacion([FromBody] AutenticacionDTO autenticacion)
        {
            _logger.LogInformation("Solicitud de autenticación recibida para usuario {Usuario}", autenticacion?.Usuario);
            
            var selec = await db.ObtenerAutenticacionAsync(autenticacion.Usuario, autenticacion.Contrasena);
            
            if (selec != null && !string.IsNullOrEmpty(selec.Usuario))
            {   
                var tokencreado = db.Token(autenticacion.Usuario);
                _logger.LogInformation("Autenticación exitosa y token generado para usuario {Usuario}", autenticacion.Usuario);
                return Ok(RespuestaApi<object>.RespuestaExitosa(
                    new { token = tokencreado },
                    MensajesRespuesta.TokenGenerado()
                ));
            }
            else
            {
                _logger.LogWarning("Intento de autenticación fallido para usuario {Usuario}", autenticacion?.Usuario);
                return Unauthorized(RespuestaApi<object>.RespuestaError(
                    MensajesRespuesta.AutenticacionFallida()    
                ));
            }
        }
        #endregion
    }
}
