using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.Modelos;
using DTO.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Dominio.Maestras.MensajesBase;

namespace PropiedadesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HistorialPropiedadController : ControllerBase
    {
        #region Atributos
        private readonly IUseCaseBase<HistorialPropiedad, int> db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public HistorialPropiedadController(IUseCaseBase<HistorialPropiedad, int> _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public async Task<ActionResult<List<HistorialPropiedadDTO>>> ObtenerTodo()
        {
            var entidades = await db.ObtenerTodoAsync();
            var dtos = _mapper.Map<List<HistorialPropiedadDTO>>(entidades);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistorialPropiedadDTO>> ObtenerPorID(int id)
        {
            var entidades = await db.ObtenerPorIDAsync(id);
            if (entidades == null)
                return NotFound();  
            var dto = _mapper.Map<HistorialPropiedadDTO>(entidades);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Insertar([FromBody] HistorialPropiedadDTO entidad)
        {
            await db.InsertarAsync(_mapper.Map<HistorialPropiedad>(entidad));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());

        }

        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult> Actualizar([FromBody] HistorialPropiedadDTO entidad)
        {
            await db.ActualizarAsync(_mapper.Map<HistorialPropiedad>(entidad));
            return Ok(Satisfactorio.Actualizado.ObtenerDeascripcionEnum());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            await db.EliminarAsync(id);
            return Ok(Satisfactorio.Eliminado.ObtenerDeascripcionEnum());
        }
        #endregion
    }
}
