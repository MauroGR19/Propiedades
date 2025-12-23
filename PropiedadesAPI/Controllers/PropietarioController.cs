using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.Comun;
using Dominio.Modelos;
using DTO.DTO;
using DTO.DTO.Comun;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PropiedadesAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar propietarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class PropietarioController : ControllerBase
    {
        #region Atributos
        private readonly IUseCasePropietario<Propietario, string> db;
        private readonly IMapper _mapper;
        private readonly ILogger<PropietarioController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del controlador de propietarios
        /// </summary>
        /// <param name="_db">Caso de uso para propietarios</param>
        /// <param name="mapper">Mapper para conversión de DTOs</param>
        /// <param name="logger">Logger para registro de eventos</param>
        public PropietarioController(IUseCasePropietario<Propietario, string> _db, IMapper mapper, ILogger<PropietarioController> logger)
        {
            db = _db;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene todos los propietarios
        /// </summary>
        /// <returns>Lista completa de propietarios</returns>
        /// <response code="200">Retorna la lista de propietarios</response>
        /// <response code="401">No autorizado</response>
        [HttpGet]
        public async Task<ActionResult<RespuestaApi<List<PropietarioDTO>>>> ObtenerTodo()
        {   
            _logger.LogInformation("Obteniendo todos los propietarios");
            var entidad = await db.ObtenerTodoAsync();
            var dtos = _mapper.Map<List<PropietarioDTO>>(entidad);
            _logger.LogInformation("Se obtuvieron {Count} propietarios", dtos.Count);
            return Ok(RespuestaApi<List<PropietarioDTO>>.RespuestaExitosa(
                dtos, 
                MensajesRespuesta.ObtenidoLista("propietarios", dtos.Count)
            ));
        }

        /// <summary>
        /// Obtiene un propietario específico por ID
        /// </summary>
        /// <param name="id">ID del propietario</param>
        /// <returns>Propietario encontrado</returns>
        /// <response code="200">Retorna el propietario encontrado</response>
        /// <response code="404">Propietario no encontrado</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaApi<PropietarioDTO>>> ObtenerPorID(string id)
        {
            _logger.LogInformation("Obteniendo propietario con ID: {Id}", id);
            var entidad = await db.ObtenerPorIDAsync(id);
            if (entidad == null)
            {
                _logger.LogWarning("No se encontró propietario con ID: {Id}", id);
                return NotFound(RespuestaApi<PropietarioDTO>.RespuestaError(
                    MensajesRespuesta.NoEncontrado("Propietario")
                ));
            }
            var dto = _mapper.Map<PropietarioDTO>(entidad); 
            _logger.LogInformation("Propietario obtenido exitosamente para ID: {Id}", id);
            return Ok(RespuestaApi<PropietarioDTO>.RespuestaExitosa(
                dto, 
                MensajesRespuesta.Obtenido("Propietario")
            ));
        }

        /// <summary>
        /// Crea un nuevo propietario
        /// </summary>
        /// <param name="entidad">Datos del propietario a crear</param>
        /// <returns>Confirmación de creación</returns>
        /// <response code="200">Propietario creado exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpPost]
        public async Task<ActionResult<RespuestaApi<object>>> Insertar([FromBody] PropietarioDTO entidad)
        {
            _logger.LogInformation("Insertando nuevo propietario: {Nombre}", entidad.Nombre);
            await db.InsertarAsync(_mapper.Map<Propietario>(entidad));
            _logger.LogInformation("Propietario insertado exitosamente: {Nombre}", entidad.Nombre);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Insertado("Propietario")));
        }

        /// <summary>
        /// Actualiza un propietario existente
        /// </summary>
        /// <param name="entidad">Datos actualizados del propietario</param>
        /// <returns>Confirmación de actualización</returns>
        /// <response code="200">Propietario actualizado exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="404">Propietario no encontrado</response>
        /// <response code="401">No autorizado</response>
        [HttpPut]
        public async Task<ActionResult<RespuestaApi<object>>> Actualizar([FromBody] PropietarioDTO entidad)
        {
            _logger.LogInformation("Actualizando propietario con documento: {NumeroDocumento}", entidad.NumeroDocumento);
            await db.ActualizarAsync(_mapper.Map<Propietario>(entidad));
            _logger.LogInformation("Propietario actualizado exitosamente con documento: {NumeroDocumento}", entidad.NumeroDocumento);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Actualizado("Propietario")));
        }

        /// <summary>
        /// Elimina un propietario por ID
        /// </summary>
        /// <param name="id">ID del propietario a eliminar</param>
        /// <returns>Confirmación de eliminación</returns>
        /// <response code="200">Propietario eliminado exitosamente</response>
        /// <response code="404">Propietario no encontrado</response>
        /// <response code="401">No autorizado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RespuestaApi<object>>> Eliminar(string id)
        {
            _logger.LogInformation("Eliminando propietario con ID: {Id}", id);
            await db.EliminarAsync(id);
            _logger.LogInformation("Propietario eliminado exitosamente con ID: {Id}", id);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Eliminado("Propietario")));
        }

        /// <summary>
        /// Obtiene propietarios con paginación
        /// </summary>
        /// <param name="request">Parámetros de paginación</param>
        /// <returns>Lista paginada de propietarios</returns>
        /// <response code="200">Retorna la lista paginada</response>
        /// <response code="400">Parámetros de paginación inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("paginado")]
        public async Task<ActionResult<RespuestaApi<PaginacionResponse<PropietarioDTO>>>> ObtenerPaginado(
            [FromQuery] PaginacionRequest request)
        {
            _logger.LogInformation("Obteniendo propietarios paginados - Página: {Pagina}, Tamaño: {TamanioPagina}", 
                request.Pagina, request.TamanioPagina);
            
            var parametros = _mapper.Map<PaginacionParametros>(request);
            var resultado = await db.ObtenerPaginadoAsync(parametros);
            var response = _mapper.Map<PaginacionResponse<PropietarioDTO>>(resultado);
            
            _logger.LogInformation("Propietarios paginados obtenidos exitosamente - Total: {Total}", 
                response.TotalRegistros);
            
            return Ok(RespuestaApi<PaginacionResponse<PropietarioDTO>>.RespuestaExitosa(
                response, 
                MensajesRespuesta.ObtenidoLista("propietarios paginados", response.TotalRegistros)
            ));
        }
        #endregion
    }
}
