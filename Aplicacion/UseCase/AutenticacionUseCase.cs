using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aplicacion.UseCase
{
    public class AutenticacionUseCase : IUseCaseAutenticacion<Autenticacion, string>
    {
        #region Atributos
        private readonly IRepositorioAutenticacion<Autenticacion, string> repositorio;
        private readonly IConfiguration _config;
        private Excepciones exception = new Excepciones();
        #endregion

        #region Constructor
        public AutenticacionUseCase(
            IRepositorioAutenticacion<Autenticacion, string> _repositorio,
            IConfiguration config)
        {
            repositorio = _repositorio;
            _config = config;
        }
        #endregion

        #region Metodos
        public Autenticacion Insertar(Autenticacion entidad)
        {
            try
            {
                entidad.contrasena = Encriptar.Encriptarr(entidad.contrasena);
                var result = repositorio.Insertar(entidad);
                repositorio.SalvarTodo();
                return result;
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, MensajesBase.Error.Insertar.ObtenerDeascripcionEnum());
            }
        }

        public Autenticacion ObtenerAutenticacion(string Usuario, string Contrasena)
        {
            Contrasena = Encriptar.Encriptarr(Contrasena);
            return repositorio.ObtenerAutenticacion(Usuario, Contrasena);
        }

        public string Token(string usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
