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
        public ActionResult<List<PropietarioDTO>> ObtenerTodo()
        {
            return Ok(_mapper.Map<List<PropietarioDTO>>(db.ObtenerTodo()));
        }

        [HttpGet("{id}")]
        public ActionResult<PropietarioDTO> ObtenetPorID(int id)
        {

            return Ok(_mapper.Map<PropietarioDTO>(db.ObtenerPorID(id)));
        }

        [HttpPost]
        public ActionResult Insertar([FromBody] PropietarioDTO entidad)
        {
            db.Insertar(_mapper.Map<Propietario>(entidad));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());

        }

        //[HttpPut("{id}")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] PropietarioDTO entidad)
        {
            db.Actualizar(_mapper.Map<Propietario>(entidad));
            return Ok(Satisfactorio.Actualizado.ObtenerDeascripcionEnum());
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            db.Eliminar(id);
            return Ok(Satisfactorio.Eliminado.ObtenerDeascripcionEnum());
        }
        #endregion
    }
}
