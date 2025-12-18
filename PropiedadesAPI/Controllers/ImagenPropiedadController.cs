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
    /// Controlador para gestionar las imágenes de propiedades
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagenPropiedadController : ControllerBase
    {
        #region Atributos
        private readonly IUseCaseImagenPropiedad<ImagenPropiedad, int> db;
        private readonly IMapper _mapper;
        private readonly ILogger<ImagenPropiedadController> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor del controlador de imágenes de propiedades
        /// </summary>
        /// <param name="_db">Caso de uso para imágenes de propiedades</param>
        /// <param name="mapper">Mapper para conversión de DTOs</param>
        /// <param name="logger">Logger para registro de eventos</param>
        public ImagenPropiedadController(IUseCaseImagenPropiedad<ImagenPropiedad, int> _db, IMapper mapper, ILogger<ImagenPropiedadController> logger)
        {
            db = _db;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene una imagen de propiedad específica por ID
        /// </summary>
        /// <param name="id">ID de la imagen de propiedad</param>
        /// <returns>Imagen de propiedad encontrada</returns>
        /// <response code="200">Retorna la imagen encontrada</response>
        /// <response code="404">Imagen no encontrada</response>
        /// <response code="401">No autorizado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<RespuestaApi<ImagenPropiedadDTO>>> ObtenerPorID(int id)
        {   
            _logger.LogInformation("Obteniendo imagen de propiedad con ID: {Id}", id);
            var entidad = await db.ObtenerPorIDAsync(id);
            if (entidad == null)
            {
                _logger.LogWarning("No se encontró imagen de propiedad con ID: {Id}", id);
                return NotFound(RespuestaApi<ImagenPropiedadDTO>.RespuestaError(
                    MensajesRespuesta.NoEncontrado("ImagenPropiedad")
                ));
            }
            var dto = _mapper.Map<ImagenPropiedadDTO>(entidad);
            _logger.LogInformation("Imagen de propiedad obtenida exitosamente para ID: {Id}", id);
            return Ok(RespuestaApi<ImagenPropiedadDTO>.RespuestaExitosa(
                dto, 
                MensajesRespuesta.Obtenido("ImagenPropiedad")
            ));
        }

        /// <summary>
        /// Crea una nueva imagen de propiedad
        /// </summary>
        /// <param name="entidad">Datos de la imagen de propiedad a crear</param>
        /// <returns>Confirmación de creación</returns>
        /// <response code="200">Imagen creada exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="401">No autorizado</response>
        [HttpPost]
        public async Task<ActionResult<RespuestaApi<object>>> Insertar([FromBody] ImagenPropiedadDTO entidad)
        {
            _logger.LogInformation("Insertando nueva imagen para PropiedadId: {PropiedadId}", entidad.IdPropiedad);
            await db.InsertarAsync(_mapper.Map<ImagenPropiedad>(entidad));
            _logger.LogInformation("Imagen de propiedad insertada exitosamente para PropiedadId: {PropiedadId}", entidad.IdPropiedad);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Insertado("ImagenPropiedad")));
        }

        /// <summary>
        /// Actualiza una imagen de propiedad existente
        /// </summary>
        /// <param name="entidad">Datos actualizados de la imagen de propiedad</param>
        /// <returns>Confirmación de actualización</returns>
        /// <response code="200">Imagen actualizada exitosamente</response>
        /// <response code="400">Datos inválidos</response>
        /// <response code="404">Imagen no encontrada</response>
        /// <response code="401">No autorizado</response>
        [HttpPut]
        public async Task<ActionResult<RespuestaApi<object>>> Actualizar([FromBody] ImagenPropiedadDTO entidad)
        {
            _logger.LogInformation("Actualizando imagen de propiedad con ID: {Id}", entidad.IdImagenPropiedad);
            await db.ActualizarAsync(_mapper.Map<ImagenPropiedad>(entidad));
            _logger.LogInformation("Imagen de propiedad actualizada exitosamente con ID: {Id}", entidad.IdImagenPropiedad);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Actualizado("ImagenPropiedad")));
        }

        /// <summary>
        /// Elimina una imagen de propiedad por ID
        /// </summary>
        /// <param name="id">ID de la imagen de propiedad a eliminar</param>
        /// <returns>Confirmación de eliminación</returns>
        /// <response code="200">Imagen eliminada exitosamente</response>
        /// <response code="404">Imagen no encontrada</response>
        /// <response code="401">No autorizado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<RespuestaApi<object>>> Eliminar(int id)
        {
            _logger.LogInformation("Eliminando imagen de propiedad con ID: {Id}", id);
            await db.EliminarAsync(id);
            _logger.LogInformation("Imagen de propiedad eliminada exitosamente con ID: {Id}", id);
            return Ok(RespuestaApi<object>.RespuestaExitosa(null, MensajesRespuesta.Eliminado("ImagenPropiedad")));
        }


        #endregion
    }
}
