using Aplicacion.UseCase;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Pruebas.UseCasePruebas
{
    public class AutenticacionUseCasePrueba
    {
        #region Atributos
        private Mock<IRepositorioAutenticacion<Autenticacion, string>> _repoMock;
        private Microsoft.Extensions.Configuration.IConfiguration _config;
        private Mock<ILogger<AutenticacionUseCase>> _loggerMock;
        private AutenticacionUseCase _useCase;
        #endregion

        #region Constructor
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IRepositorioAutenticacion<Autenticacion, string>>();
            _loggerMock = new Mock<ILogger<AutenticacionUseCase>>();

            // Config in-memory para JWT
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string,string>("Jwt:Key", "ClaveSuperSecreta_Muy_Larga_Con_Entropia"),
                    new KeyValuePair<string,string>("Jwt:Issuer", "PropiedadesAPI"),
                    new KeyValuePair<string,string>("Jwt:Audience", "PropiedadesAPI-Clients"),
                })
                .Build();

            _useCase = new AutenticacionUseCase(_repoMock.Object, _config, _loggerMock.Object);
        }
        #endregion

        #region Metodos
        [Test]
        public async Task Insertar_ContrasenaSeEncripta_YGuardaEnRepositorio()
        {
            // Arrange
            var entrada = new Autenticacion { Usuario = "mauri", Contrasena = "1234" };
            Autenticacion capturada = null!;

            _repoMock
                .Setup(r => r.InsertarAsync(It.IsAny<Autenticacion>()))
                .Callback<Autenticacion>(a => capturada = a)
                .ReturnsAsync((Autenticacion a) => a);

            _repoMock.Setup(r => r.SalvarTodoAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.InsertarAsync(entrada);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(capturada);
            Assert.IsTrue(Encriptar.VerifyPassword("1234", capturada.Contrasena), 
                "La contraseña debe estar hasheada correctamente");
            Assert.AreNotEqual("1234", capturada.Contrasena, 
                "La contraseña NO debe estar en texto plano");
            _repoMock.Verify(r => r.InsertarAsync(It.IsAny<Autenticacion>()), Times.Once);
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Once);
        }

        [Test]
        public void Insertar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock
                .Setup(r => r.InsertarAsync(It.IsAny<Autenticacion>()))
                .ThrowsAsync(new InvalidOperationException("fallo repo"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.InsertarAsync(new Autenticacion { Usuario = "u", Contrasena = "p" }));
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Never);
        }

        [Test]
        public async Task ObtenerAutenticacion_UsuarioValido_RetornaUsuario()
        {
            // Arrange
            var usuario = "mauri";
            var passPlano = "1234";
            var passHash = Encriptar.HashPassword(passPlano);
            var usuarioDB = new Autenticacion { Usuario = usuario, Contrasena = passHash };

            _repoMock
                .Setup(r => r.ObtenerPorUsuarioAsync(usuario))
                .ReturnsAsync(usuarioDB);

            // Act
            var result = await _useCase.ObtenerAutenticacionAsync(usuario, passPlano);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(usuario, result.Usuario);
            _repoMock.Verify(r => r.ObtenerPorUsuarioAsync(usuario), Times.Once);
        }

        [Test]
        public void ObtenerAutenticacion_ContrasenaIncorrecta_LanzaExcepcion()
        {
            // Arrange
            var usuario = "mauri";
            var passCorrecta = "1234";
            var passIncorrecta = "incorrecta";
            var passHash = Encriptar.HashPassword(passCorrecta);
            var usuarioDB = new Autenticacion { Usuario = usuario, Contrasena = passHash };

            _repoMock
                .Setup(r => r.ObtenerPorUsuarioAsync(usuario))
                .ReturnsAsync(usuarioDB);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ObtenerAutenticacionAsync(usuario, passIncorrecta));
            _repoMock.Verify(r => r.ObtenerPorUsuarioAsync(usuario), Times.Once);
        }

        [Test]
        public void ObtenerAutenticacion_UsuarioNoExiste_LanzaExcepcion()
        {
            // Arrange
            var usuario = "noexiste";
            var pass = "1234";

            _repoMock
                .Setup(r => r.ObtenerPorUsuarioAsync(usuario))
                .ReturnsAsync((Autenticacion)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ObtenerAutenticacionAsync(usuario, pass));
            _repoMock.Verify(r => r.ObtenerPorUsuarioAsync(usuario), Times.Once);
        }

        [Test]
        public void Token_GeneraJwt_ConIssuerAudienceSubjectYExpiracion()
        {
            // Arrange
            var usuario = "mauri";

            // Act
            var tokenStr = _useCase.Token(usuario);

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenStr);

            // Subject
            var sub = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            Assert.AreEqual(usuario, sub);

            // Issuer / Audience
            Assert.AreEqual(_config["Jwt:Issuer"], token.Issuer);
            Assert.IsTrue(token.Audiences.Contains(_config["Jwt:Audience"]));

            // Expiración ~1h (tolerancia ±2 min)
            var ahora = DateTime.UtcNow;
            var min = ahora.AddMinutes(58);
            var max = ahora.AddMinutes(62);
            Assert.IsTrue(token.ValidTo >= min && token.ValidTo <= max,
                $"ValidTo {token.ValidTo:o} fuera del rango [{min:o}, {max:o}]");
        }
        #endregion
    }
}