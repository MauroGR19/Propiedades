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
        public ActionResult<List<HistorialPropiedadDTO>> ObtenerTodo()
        {
            return Ok(_mapper.Map<List<HistorialPropiedadDTO>>(db.ObtenerTodo()));
        }

        [HttpGet("{id}")]
        public ActionResult<HistorialPropiedadDTO> ObtenetPorID(int id)
        {

            return Ok(_mapper.Map<HistorialPropiedadDTO>(db.ObtenerPorID(id)));
        }

        [HttpPost]
        public ActionResult Insertar([FromBody] HistorialPropiedadDTO entidad)
        {
            db.Insertar(_mapper.Map<HistorialPropiedad>(entidad));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());

        }

        //[HttpPut("{id}")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] HistorialPropiedadDTO entidad)
        {
            db.Actualizar(_mapper.Map<HistorialPropiedad>(entidad));
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
