using Aplicacion.UseCase;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Pruebas.UseCasePruebas
{
    public class ImagenPropiedadUseCasePrueba
    {
        #region Atributos
        private Mock<IRepositorioImagenPropiedad<ImagenPropiedad, int>> _repoMock;
        private ImagenPropiedadUseCase _useCase;
        #endregion

        #region Constructor
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IRepositorioImagenPropiedad<ImagenPropiedad, int>>();
            _useCase = new ImagenPropiedadUseCase(_repoMock.Object);
        }
        #endregion

        #region Metodos

        [Test]
        public async Task Insertar_EntidadValida_RetornaEntidadYGuarda()
        {
            // Arrange
            var entidad = new ImagenPropiedad { IdImagenPropiedad = 1, Archivo = "img1.jpg" };
            _repoMock.Setup(r => r.InsertarAsync(entidad)).ReturnsAsync(entidad);
            _repoMock.Setup(r => r.SalvarTodoAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.InsertarAsync(entidad);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.IdImagenPropiedad);
            _repoMock.Verify(r => r.InsertarAsync(entidad), Times.Once);
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Once);
        }

        [Test]
        public void Insertar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.InsertarAsync(It.IsAny<ImagenPropiedad>()))
                     .ThrowsAsync(new InvalidOperationException("falló repo"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.InsertarAsync(new ImagenPropiedad()));
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Never);
        }

        [Test]
        public async Task Actualizar_Valido_RetornaTrueYGuarda()
        {
            // Arrange
            var entidad = new ImagenPropiedad { IdImagenPropiedad = 2, Archivo = "img2.jpg" };
            _repoMock.Setup(r => r.ActualizarAsync(entidad)).ReturnsAsync(true);
            _repoMock.Setup(r => r.SalvarTodoAsync()).Returns(Task.CompletedTask);

            // Act
            var ok = await _useCase.ActualizarAsync(entidad);

            // Assert
            Assert.IsTrue(ok);
            _repoMock.Verify(r => r.ActualizarAsync(entidad), Times.Once);
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Once);
        }

        [Test]
        public void Actualizar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.ActualizarAsync(It.IsAny<ImagenPropiedad>()))
                     .ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ActualizarAsync(new ImagenPropiedad()));
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Never);
        }

        [Test]
        public async Task Eliminar_Valido_RetornaTrueYGuarda()
        {
            // Arrange
            _repoMock.Setup(r => r.EliminarAsync(10)).ReturnsAsync(true);
            _repoMock.Setup(r => r.SalvarTodoAsync()).Returns(Task.CompletedTask);

            // Act
            var ok = await _useCase.EliminarAsync(10);

            // Assert
            Assert.IsTrue(ok);
            _repoMock.Verify(r => r.EliminarAsync(10), Times.Once);
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Once);
        }

        [Test]
        public void Eliminar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.EliminarAsync(It.IsAny<int>()))
                     .ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.EliminarAsync(99));
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Never);
        }

        [Test]
        public async Task ObtenerPorID_Valido_RetornaEntidad()
        {
            // Arrange
            var entidad = new ImagenPropiedad { IdImagenPropiedad = 5, Archivo = "img5.jpg" };
            _repoMock.Setup(r => r.ObtenerPorIDAsync(5)).ReturnsAsync(entidad);

            // Act
            var result = await _useCase.ObtenerPorIDAsync(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.IdImagenPropiedad);
            _repoMock.Verify(r => r.ObtenerPorIDAsync(5), Times.Once);
        }

        [Test]
        public void ObtenerPorID_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.ObtenerPorIDAsync(It.IsAny<int>()))
                     .ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ObtenerPorIDAsync(1));
        }

        #endregion
    }
}