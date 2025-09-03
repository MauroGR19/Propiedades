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
    public class ImagenPropiedadController : ControllerBase
    {
        #region Atributos
        private readonly IUseCaseImagenPropiedad<ImagenPropiedad, int> db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ImagenPropiedadController(IUseCaseImagenPropiedad<ImagenPropiedad, int> _db, IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        [HttpGet("{id}")]
        public ActionResult<ImagenPropiedadDTO> ObtenetPorID(int id)
        {

            return Ok(_mapper.Map<ImagenPropiedadDTO>(db.ObtenerPorID(id)));
        }

        [HttpPost]
        public ActionResult Insertar([FromBody] ImagenPropiedadDTO entidad)
        {
            db.Insertar(_mapper.Map<ImagenPropiedad>(entidad));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());

        }

        //[HttpPut("{id}")]
        [HttpPut]
        public ActionResult Actualizar([FromBody] ImagenPropiedadDTO entidad)
        {
            db.Actualizar(_mapper.Map<ImagenPropiedad>(entidad));
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
