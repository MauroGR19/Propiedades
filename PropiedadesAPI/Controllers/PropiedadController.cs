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
    /// Controlador para gestionar propiedades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropiedadController : ControllerBase
    {
        #region Atributos
        private readonly IUseCasePropiedad<Propiedad, string> _svc;
        private readonly IMapper _mapper;
        private readonly ILogger<PropiedadController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del controlador de propiedades
        /// </summary>
        /// <param name="svc">Caso de uso para propiedades</param>
        /// <param name="mapper">Mapper para conversión de DTOs</param>
        /// <param name="logger">Logger para registro de eventos</param>
        public PropiedadController(IUseCasePropiedad<Propiedad, string> svc, IMapper mapper, ILogger<PropiedadController> logger)
        {
            _svc = svc;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene todas las propiedades
        /// </summary>
        /// <returns>Lista completa de propiedades</returns>
        /// <response code="200">Retorna la lista de propiedades</response>
        /// <response code="401">No autorizado</response>
        [HttpGet]
        public async Task<ActionResult<RespuestaApi<IEnumerable<PropiedadDTO>>>> ObtenerTodo()
        {
            _logger.LogInformation("Obteniendo todas las propiedades");
            var entidades = await _svc.ObtenerTodoAsync(); 
            var dtos = _mapper.Map<IEnumerable<PropiedadDTO>>(entidades);
            _logger.LogInformation("Se obtuvieron {Count} propiedades", dtos.Count());
            return Ok(RespuestaApi<IEnumerable<PropiedadDTO>>.RespuestaExitosa(
                dtos, 
                MensajesRespuesta.ObtenidoLista("propiedades", dtos.Count())
            ));
        }

        /// <summary>
        /// Obtiene una propiedad específica por ID
        /// </summary>
        /// <param name="id">ID de la propiedad</param>
        /// <returns>Propiedad encontrada</returns>
        /// <response code="200">Retorna la propiedad encontrada</response>
        /// <response code="404">Propiedad no encontrada</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaApi<PropiedadDTO>>> ObtenerPorID(string id)
        {
            _logger.LogInformation("Obteniendo propiedad con ID: {Id}", id);
            var entidad = await _svc.ObtenerPorIDAsync(id);
            if (entidad is null)
            {
                _logger.LogWarning("No se encontró propiedad con ID: {Id}", id);
                return NotFound(RespuestaApi<PropiedadDTO>.RespuestaError(
                    MensajesRespuesta.NoEncontrado("Propiedad")
                ));
            }
            var dto = _mapper.Map<PropiedadDTO>(entidad);
            _logger.LogInformation("Propiedad obtenida exitosamente para ID: {Id}", id);
            return Ok(RespuestaApi<PropiedadDTO>.RespuestaExitosa(
                dto, 
                MensajesRespuesta.Obtenido("Propiedad")
            ));
        }

        /// <summary>
        /// Obtiene propiedades aplicando filtros
        /// </summary>
        /// <param name="order">Orden de resultados (ASC o DESC)</param>
        /// <param name="direccion">Filtro por dirección</param>
        /// <param name="precio">Filtro por precio</param>
        /// <param name="anio">Filtro por año</param>
        /// <returns>Lista de propiedades filtradas</returns>
        /// <response code="200">Retorna la lista filtrada</response>
        /// <response code="400">Parámetros de filtro inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("filtrar")]
        public async Task<ActionResult<RespuestaApi<IEnumerable<PropiedadDTO>>>> ObtenerPorFiltro(
            [FromQuery] string order = "ASC",
            [FromQuery] string? direccion = null,
            [FromQuery] decimal? precio = null,
            [FromQuery] int? anio = null)
        {
            _logger.LogInformation("Filtrando propiedades con orden: {Order}, dirección: {Direccion}, precio: {Precio}, año: {Anio}", 
                order, direccion, precio, anio);
            var filtroDto = new PropiedadDTO
            {
                Direccion = direccion ?? "",
                Precio = precio ?? 0,
                Anio = anio ?? 0
            };

            var filtroEntidad = _mapper.Map<Propiedad>(filtroDto);
            var result = await _svc.ObtenerPorFiltroAsync(filtroEntidad, order); 
            var dtos = _mapper.Map<IEnumerable<PropiedadDTO>>(result);
            _logger.LogInformation("Se obtuvieron {Count} propiedades filtradas", dtos.Count());
            return Ok(RespuestaApi<IEnumerable<PropiedadDTO>>.RespuestaExitosa(
                dtos, 
                MensajesRespuesta.ObtenidoLista("propiedades filtradas", dtos.Count())
            ));
        }

        /// <summary>
        /// Crea una nueva propiedad
        /// </summary>
        /// <param name="dto">Datos de la propiedad a crear</param>
        /// <returns>Confirmación de creación</returns>
        /// <response code="200">Propiedad creada exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpPost]
        public async Task<ActionResult<RespuestaApi<object>>> Insertar([FromBody] PropiedadDTO dto)
        {
            _logger.LogInformation("Insertando nueva propiedad en dirección: {Direccion}", dto.Direccion);
            await _svc.InsertarAsync(_mapper.Map<Propiedad>(dto));
            _logger.LogInformation("Propiedad insertada exitosamente en dirección: {Direccion}", dto.Direccion);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Insertado("Propiedad")));
        }

        /// <summary>
        /// Actualiza una propiedad existente
        /// </summary>
        /// <param name="dto">Datos actualizados de la propiedad</param>
        /// <returns>Confirmación de actualización</returns>
        /// <response code="200">Propiedad actualizada exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="404">Propiedad no encontrada</response>
        /// <response code="401">No autorizado</response>
        [HttpPut]
        public async Task<ActionResult<RespuestaApi<object>>> Actualizar([FromBody] PropiedadDTO dto)
        {
            _logger.LogInformation("Actualizando propiedad con ID: {Id}", dto.MatriculaInmobiliaria);
            await _svc.ActualizarAsync(_mapper.Map<Propiedad>(dto));
            _logger.LogInformation("Propiedad actualizada exitosamente con ID: {Id}", dto.MatriculaInmobiliaria);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Actualizado("Propiedad")));
        }

        /// <summary>
        /// Elimina una propiedad por ID
        /// </summary>
        /// <param name="id">ID de la propiedad a eliminar</param>
        /// <returns>Confirmación de eliminación</returns>
        /// <response code="200">Propiedad eliminada exitosamente</response>
        /// <response code="404">Propiedad no encontrada</response>
        /// <response code="401">No autorizado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RespuestaApi<object>>> Eliminar(string id)
        {
            _logger.LogInformation("Eliminando propiedad con ID: {Id}", id);
            await _svc.EliminarAsync(id);
            _logger.LogInformation("Propiedad eliminada exitosamente con ID: {Id}", id);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Eliminado("Propiedad")));
        }

        /// <summary>
        /// Obtiene propiedades con paginación
        /// </summary>
        /// <param name="request">Parámetros de paginación</param>
        /// <returns>Lista paginada de propiedades</returns>
        /// <response code="200">Retorna la lista paginada</response>
        /// <response code="400">Parámetros de paginación inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("paginado")]
        public async Task<ActionResult<RespuestaApi<PaginacionResponse<PropiedadDTO>>>> ObtenerPaginado(
            [FromQuery] PaginacionRequest request)
        {
            _logger.LogInformation("Obteniendo propiedades paginadas - Página: {Pagina}, Tamaño: {TamanioPagina}", 
                request.Pagina, request.TamanioPagina);
            
            var parametros = _mapper.Map<PaginacionParametros>(request);
            var resultado = await _svc.ObtenerPaginadoAsync(parametros);
            var response = _mapper.Map<PaginacionResponse<PropiedadDTO>>(resultado);
            
            _logger.LogInformation("Propiedades paginadas obtenidas exitosamente - Total: {Total}", 
                response.TotalRegistros);
            
            return Ok(RespuestaApi<PaginacionResponse<PropiedadDTO>>.RespuestaExitosa(
                response, 
                MensajesRespuesta.ObtenidoLista("propiedades paginadas", response.TotalRegistros)
            ));
        }
        #endregion
    }
}
