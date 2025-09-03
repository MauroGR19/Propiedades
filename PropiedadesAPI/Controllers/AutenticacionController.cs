﻿using Aplicacion.Interfaces;
using AutoMapper;
using Dominio.Modelos;
using DTO.DTO;
using Microsoft.AspNetCore.Mvc;
using static Dominio.Maestras.MensajesBase;

namespace PropiedadesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //Cuando el end point retorna el token y se quiera probar en Swagger se debe poner Bearer antes del token en la Autorizacion, ejm
    //Bearer eyJhbGciOiJIUzI1NiIsI
    //Si se va a probar en postman, el token se pone en el tipo de autorizacion Bearer Token, y ahí se pone el token

    public class AutenticacionController : ControllerBase
    {
        #region Atributos
        private readonly IUseCaseAutenticacion<Autenticacion, string> db;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AutenticacionController(IUseCaseAutenticacion<Autenticacion, string> _db , IMapper mapper)
        {
            db = _db;
            _mapper = mapper;
        }
        #endregion

        #region Metodos
        [HttpPost]
        public ActionResult Insertar([FromBody] AutenticacionDTO entidad)
        {
            db.Insertar(_mapper.Map<Autenticacion>(entidad));
            return Ok(Satisfactorio.Insertado.ObtenerDeascripcionEnum());

        }

        [HttpPost]
        [Route("Validar")]
        public ActionResult ObtenerAutenticacion([FromBody] AutenticacionDTO autenticacion)
        {
            var selec = _mapper.Map<Autenticacion>(db.ObtenerAutenticacion(autenticacion.usuario, autenticacion.contrasena));

            if (selec != null)
            {
                var tokencreado = db.Token(autenticacion.usuario);
                return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
        }
        #endregion
    }
}
