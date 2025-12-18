using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.Modelos;
using DTO.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Dominio.Maestras.MensajesBase;
using System.Threading.Tasks;

namespace PropiedadesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropiedadController : ControllerBase
    {
        #region Atributos
        private readonly IUseCasePropiedad<Propiedad, int> _svc;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PropiedadController(IUseCasePropiedad<Propiedad, int> svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropiedadDTO>>> ObtenerTodo()
        {
            var entidades = await _svc.ObtenerTodoAsync(); 
            var dtos = _mapper.Map<IEnumerable<PropiedadDTO>>(entidades);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PropiedadDTO>> ObtenerPorID(int id)
        {
            var entidad = await _svc.ObtenerPorIDAsync(id);
            if (entidad is null) 
                return NotFound();
            var dto = _mapper.Map<PropiedadDTO>(entidad);
            return Ok(dto);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<PropiedadDTO>>> ObtenerPorFiltro(
            [FromQuery] string order = "ASC",
            [FromQuery] string? direccion = null,
            [FromQuery] decimal? precio = null,
            [FromQuery] int? anio = null)
        {
            var filtroDto = new PropiedadDTO
            {
                Direccion = direccion ?? "",
                Precio = precio ?? 0,
                Anio = anio ?? 0
            };

            var filtroEntidad = _mapper.Map<Propiedad>(filtroDto);
            var result = await _svc.ObtenerPorFiltroAsync(filtroEntidad, order); 
            var dtos = _mapper.Map<IEnumerable<PropiedadDTO>>(result);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Insertar([FromBody] PropiedadDTO dto)
        {
            await _svc.InsertarAsync(_mapper.Map<Propiedad>(dto));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());
        }

        [HttpPut]
        public async Task<ActionResult> Actualizar([FromBody] PropiedadDTO dto)
        {
            await _svc.ActualizarAsync(_mapper.Map<Propiedad>(dto));
            return Ok(Satisfactorio.Actualizado.ObtenerDeascripcionEnum());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await _svc.EliminarAsync(id);
            return Ok(Satisfactorio.Eliminado.ObtenerDeascripcionEnum());
        }
        #endregion
    }
}
