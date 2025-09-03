using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.Modelos;
using DTO.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Dominio.Maestras.MensajesBase;

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
        public ActionResult<IEnumerable<PropiedadDTO>> ObtenerTodo()
        {
            var entidades = _svc.ObtenerTodo(); 
            var dtos = _mapper.Map<IEnumerable<PropiedadDTO>>(entidades);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PropiedadDTO> ObtenerPorID(int id)
        {
            var entidad = _svc.ObtenerPorID(id);
            if (entidad is null) return NotFound();
            var dto = _mapper.Map<PropiedadDTO>(entidad);
            return Ok(dto);
        }

        [HttpGet("filtrar")]
        public ActionResult<IEnumerable<PropiedadDTO>> ObtenerPorFiltro(
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
            var result = _svc.ObtenerPorFiltro(filtroEntidad, order); 
            var dtos = _mapper.Map<IEnumerable<PropiedadDTO>>(result);
            return Ok(dtos);
        }

        [HttpPost]
        public ActionResult Insertar([FromBody] PropiedadDTO dto)
        {
            _svc.Insertar(_mapper.Map<Propiedad>(dto));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());
        }

        [HttpPut]
        public ActionResult Actualizar([FromBody] PropiedadDTO dto)
        {
            _svc.Actualizar(_mapper.Map<Propiedad>(dto));
            return Ok(Satisfactorio.Actualizado.ObtenerDeascripcionEnum());
        }

        [HttpDelete("{id:int}")]
        public ActionResult Eliminar(int id)
        {
            _svc.Eliminar(id);
            return Ok(Satisfactorio.Eliminado.ObtenerDeascripcionEnum());
        }
        #endregion
    }
}
