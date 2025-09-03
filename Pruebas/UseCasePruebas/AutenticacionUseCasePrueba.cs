using Aplicacion.UseCase;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace Pruebas.UseCasePruebas
{
    public class AutenticacionUseCasePrueba
    {
        #region Atributos
        private Mock<IRepositorioAutenticacion<Autenticacion, string>> _repoMock;
        private Microsoft.Extensions.Configuration.IConfiguration _config;
        private AutenticacionUseCase _useCase;
        #endregion

        #region Constructor
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IRepositorioAutenticacion<Autenticacion, string>>();

            // Config in-memory para JWT
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string,string>("Jwt:Key", "ClaveSuperSecreta_Muy_Larga_Con_Entropia"),
                    new KeyValuePair<string,string>("Jwt:Issuer", "PropiedadesAPI"),
                    new KeyValuePair<string,string>("Jwt:Audience", "PropiedadesAPI-Clients"),
                })
                .Build();

            _useCase = new AutenticacionUseCase(_repoMock.Object, _config);
        }
        #endregion

        #region Metodos
        [Test]
        public void Insertar_ContrasenaSeEncripta_YGuardaEnRepositorio()
        {
            // Arrange
            var entrada = new Autenticacion { usuario = "mauri", contrasena = "1234" };
            Autenticacion capturada = null!;

            _repoMock
                .Setup(r => r.Insertar(It.IsAny<Autenticacion>()))
                .Callback<Autenticacion>(a => capturada = a)
                .Returns<Autenticacion>(a => a);

            _repoMock.Setup(r => r.SalvarTodo());

            // Act
            var result = _useCase.Insertar(entrada);

            // Assert
            var esperado = Encriptar.Encriptarr("1234");
            Assert.IsNotNull(result);
            Assert.AreEqual(esperado, capturada.contrasena, "La contraseña debe llegar encriptada al repositorio");
            Assert.AreEqual(esperado, result.contrasena, "El resultado debe conservar la contraseña encriptada");
            _repoMock.Verify(r => r.Insertar(It.IsAny<Autenticacion>()), Times.Once);
            _repoMock.Verify(r => r.SalvarTodo(), Times.Once);
        }

        [Test]
        public void Insertar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock
                .Setup(r => r.Insertar(It.IsAny<Autenticacion>()))
                .Throws(new InvalidOperationException("fallo repo"));

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.Insertar(new Autenticacion { usuario = "u", contrasena = "p" }));
            _repoMock.Verify(r => r.SalvarTodo(), Times.Never);
        }

        [Test]
        public void ObtenerAutenticacion_EncriptaAntesDeConsultarRepositorio()
        {
            // Arrange
            var usuario = "mauri";
            var passPlano = "1234";
            var passEnc = Encriptar.Encriptarr(passPlano);
            var esperado = new Autenticacion { usuario = usuario, contrasena = passEnc };

            _repoMock
                .Setup(r => r.ObtenerAutenticacion(usuario, passEnc))
                .Returns(esperado);

            // Act
            var result = _useCase.ObtenerAutenticacion(usuario, passPlano);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(usuario, result.usuario);
            Assert.AreEqual(passEnc, result.contrasena);
            _repoMock.Verify(r => r.ObtenerAutenticacion(usuario, passEnc), Times.Once);
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
