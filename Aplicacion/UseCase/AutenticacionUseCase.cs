using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dominio.Excepciones;
using Dominio.Comun;

namespace Aplicacion.UseCase
{
    public class AutenticacionUseCase : IUseCaseAutenticacion<Autenticacion, string>
    {
        #region Atributos
        private readonly IRepositorioAutenticacion<Autenticacion, string> repositorio;
        private readonly IConfiguration _config;
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
        public async Task<Autenticacion> InsertarAsync(Autenticacion entidad)
        {
            Guard.NoNuloOVacio(entidad.Usuario, "Usuario");
            Guard.NoNuloOVacio(entidad.Contrasena, "Contraseña");
            Guard.LongitudMinima(entidad.Usuario, 5, "Usuario");

            try
            {
                entidad.Contrasena = Encriptar.HashPassword(entidad.Contrasena);
                var result = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new AutenticacionException($"Error al crear usuario: {ex.Message}");
            }
        }

        public async Task<Autenticacion> ObtenerAutenticacionAsync(string Usuario, string Contrasena)
        {
            Guard.NoNuloOVacio(Usuario, "Usuario");
            Guard.NoNuloOVacio(Contrasena, "Contraseña");

            var usuario = await repositorio.ObtenerPorUsuarioAsync(Usuario);

            Guard.NoNulo(usuario, "Usuario", "Usuario no encontrado");
            Guard.NoNuloOVacio(usuario.Contrasena, "Contraseña", "Usuario sin contraseña configurada");

            bool esValido = Encriptar.VerifyPassword(Contrasena, usuario.Contrasena);
            if (!esValido)
                throw new AutenticacionException("Contraseña incorrecta");

            return usuario;

        }

        public string Token(string usuario)
        {
            Guard.NoNuloOVacio(usuario, "Usuario", "No puede estar vacío");

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
