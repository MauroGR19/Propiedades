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
    /// Controlador para gestionar el historial de propiedades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistorialPropiedadController : ControllerBase
    {
        #region Atributos
        private readonly IUseCaseHistorialPropiedad<HistorialPropiedad, int> db;
        private readonly IMapper _mapper;
        private readonly ILogger<HistorialPropiedadController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del controlador de historial de propiedades
        /// </summary>
        /// <param name="_db">Caso de uso para historial de propiedades</param>
        /// <param name="mapper">Mapper para conversión de DTOs</param>
        /// <param name="logger">Logger para registro de eventos</param>
        public HistorialPropiedadController(IUseCaseHistorialPropiedad<HistorialPropiedad, int> _db, IMapper mapper, ILogger<HistorialPropiedadController> logger)
        {
            db = _db;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene todo el historial de propiedades
        /// </summary>
        /// <returns>Lista completa del historial de propiedades</returns>
        /// <response code="200">Retorna la lista de historiales</response>
        /// <response code="401">No autorizado</response>
        [HttpGet]
        public async Task<ActionResult<RespuestaApi<List<HistorialPropiedadDTO>>>> ObtenerTodo()
        {
            _logger.LogInformation("Obteniendo todo el historial de propiedades");
            var entidades = await db.ObtenerTodoAsync();
            var dtos = _mapper.Map<List<HistorialPropiedadDTO>>(entidades);
            _logger.LogInformation("Se obtuvieron {Count} registros de historial", dtos.Count);
            return Ok(RespuestaApi<List<HistorialPropiedadDTO>>.RespuestaExitosa(dtos, MensajesRespuesta.ObtenidoLista("HistorialPropiedad", dtos.Count)));
        }

        /// <summary>
        /// Obtiene un historial de propiedad específico por ID
        /// </summary>
        /// <param name="id">ID del historial de propiedad</param>
        /// <returns>Historial de propiedad encontrado</returns>
        /// <response code="200">Retorna el historial encontrado</response>
        /// <response code="404">Historial no encontrado</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaApi<HistorialPropiedadDTO>>> ObtenerPorID(int id)
        {
            _logger.LogInformation("Obteniendo historial de propiedad con ID: {Id}", id);
            var entidades = await db.ObtenerPorIDAsync(id);
            if (entidades == null)
            {
                _logger.LogWarning("No se encontró historial de propiedad con ID: {Id}", id);
                return NotFound(RespuestaApi<HistorialPropiedadDTO>.RespuestaError(MensajesRespuesta.NoEncontrado("HistorialPropiedad")));
            }
            var dto = _mapper.Map<HistorialPropiedadDTO>(entidades);
            _logger.LogInformation("Historial de propiedad obtenido exitosamente para ID: {Id}", id);
            return Ok(RespuestaApi<HistorialPropiedadDTO>.RespuestaExitosa(dto, MensajesRespuesta.Obtenido("HistorialPropiedad")));
        }

        /// <summary>
        /// Crea un nuevo historial de propiedad
        /// </summary>
        /// <param name="entidad">Datos del historial de propiedad a crear</param>
        /// <returns>Confirmación de creación</returns>
        /// <response code="200">Historial creado exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpPost]
        public async Task<ActionResult<RespuestaApi<object>>> Insertar([FromBody] HistorialPropiedadDTO entidad)
        {
            _logger.LogInformation("Insertando nuevo historial de propiedad para PropiedadId: {PropiedadId}", entidad.MatriculaInmobiliaria);
            await db.InsertarAsync(_mapper.Map<HistorialPropiedad>(entidad));
            _logger.LogInformation("Historial de propiedad insertado exitosamente para PropiedadId: {PropiedadId}", entidad.MatriculaInmobiliaria);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Insertado("HistorialPropiedad")));
        }

        /// <summary>
        /// Actualiza un historial de propiedad existente
        /// </summary>
        /// <param name="entidad">Datos actualizados del historial de propiedad</param>
        /// <returns>Confirmación de actualización</returns>
        /// <response code="200">Historial actualizado exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="404">Historial no encontrado</response>
        /// <response code="401">No autorizado</response>
        [HttpPut]
        public async Task<ActionResult<RespuestaApi<object>>> Actualizar([FromBody] HistorialPropiedadDTO entidad)
        {
            _logger.LogInformation("Actualizando historial de propiedad con ID: {Id}", entidad.IdHistorialPropiedad);
            await db.ActualizarAsync(_mapper.Map<HistorialPropiedad>(entidad));
            _logger.LogInformation("Historial de propiedad actualizado exitosamente con ID: {Id}", entidad.IdHistorialPropiedad);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Actualizado("HistorialPropiedad")));
        }

        /// <summary>
        /// Elimina un historial de propiedad por ID
        /// </summary>
        /// <param name="id">ID del historial de propiedad a eliminar</param>
        /// <returns>Confirmación de eliminación</returns>
        /// <response code="200">Historial eliminado exitosamente</response>
        /// <response code="404">Historial no encontrado</response>
        /// <response code="401">No autorizado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RespuestaApi<object>>> Eliminar(int id)
        {
            _logger.LogInformation("Eliminando historial de propiedad con ID: {Id}", id);
            await db.EliminarAsync(id);
            _logger.LogInformation("Historial de propiedad eliminado exitosamente con ID: {Id}", id);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Eliminado("HistorialPropiedad")));
        }

        /// <summary>
        /// Obtiene historiales de propiedades con paginación
        /// </summary>
        /// <param name="request">Parámetros de paginación</param>
        /// <returns>Lista paginada de historiales de propiedades</returns>
        /// <response code="200">Retorna la lista paginada</response>
        /// <response code="400">Parámetros de paginación inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("paginado")]
        public async Task<ActionResult<RespuestaApi<PaginacionResponse<HistorialPropiedadDTO>>>> ObtenerPaginado(
            [FromQuery] PaginacionRequest request)
        {
            _logger.LogInformation("Obteniendo historiales de propiedades paginados - Página: {Pagina}, Tamaño: {TamanioPagina}", 
                request.Pagina, request.TamanioPagina);
            
            var parametros = _mapper.Map<PaginacionParametros>(request);
            var resultado = await db.ObtenerPaginadoAsync(parametros);
            var response = _mapper.Map<PaginacionResponse<HistorialPropiedadDTO>>(resultado);
            
            _logger.LogInformation("Historiales de propiedades paginados obtenidos exitosamente - Total: {Total}", 
                response.TotalRegistros);
            
            return Ok(RespuestaApi<PaginacionResponse<HistorialPropiedadDTO>>.RespuestaExitosa(
                response, 
                MensajesRespuesta.ObtenidoLista("historiales paginados", response.TotalRegistros)
            ));
        }
        #endregion
    }
}
