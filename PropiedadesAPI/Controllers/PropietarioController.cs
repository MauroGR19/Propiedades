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
    public class PropietarioController : ControllerBase
    {
        #region Atributos
        private readonly IUseCaseBase<Propietario, int> db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PropietarioController(IUseCaseBase<Propietario, int> _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        [HttpGet]
        public async Task<ActionResult<List<PropietarioDTO>>> ObtenerTodo()
        {   
            var entidad = await db.ObtenerTodoAsync();
            var dtos = _mapper.Map<List<PropietarioDTO>>(entidad);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PropietarioDTO>> ObtenerPorID(int id)
        {
            var entidad = await db.ObtenerPorIDAsync(id);
            if (entidad == null)
                return NotFound();
            var dto = _mapper.Map<PropietarioDTO>(entidad); 
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Insertar([FromBody] PropietarioDTO entidad)
        {
            await db.InsertarAsync(_mapper.Map<Propietario>(entidad));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());

        }

        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult> Actualizar([FromBody] PropietarioDTO entidad)
        {
            await db.ActualizarAsync(_mapper.Map<Propietario>(entidad));
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
