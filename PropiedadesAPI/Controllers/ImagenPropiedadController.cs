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
        public async Task<ActionResult<ImagenPropiedadDTO>> ObtenerPorID(int id)
        {   
            var entidad = await db.ObtenerPorIDAsync(id);
            if (entidad == null)
                return NotFound();
            var dto = _mapper.Map<ImagenPropiedadDTO>(entidad);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Insertar([FromBody] ImagenPropiedadDTO entidad)
        {
            await db.InsertarAsync(_mapper.Map<ImagenPropiedad>(entidad));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());

        }

        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<ActionResult> Actualizar([FromBody] ImagenPropiedadDTO entidad)
        {
            await db.ActualizarAsync(_mapper.Map<ImagenPropiedad>(entidad));
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
