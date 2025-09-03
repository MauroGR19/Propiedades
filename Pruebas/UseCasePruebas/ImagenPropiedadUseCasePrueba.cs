using Aplicacion.UseCase;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Moq;

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
        public void Insertar_EntidadValida_RetornaEntidadYGuarda()
        {
            // Arrange
            var entidad = new ImagenPropiedad { IdImagenPropiedad = 1, Archivo = "img1.jpg" };
            _repoMock.Setup(r => r.Insertar(entidad)).Returns(entidad);
            _repoMock.Setup(r => r.SalvarTodo());

            // Act
            var result = _useCase.Insertar(entidad);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.IdImagenPropiedad);
            _repoMock.Verify(r => r.Insertar(entidad), Times.Once);
            _repoMock.Verify(r => r.SalvarTodo(), Times.Once);
        }

        [Test]
        public void Insertar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.Insertar(It.IsAny<ImagenPropiedad>()))
                     .Throws(new InvalidOperationException("falló repo"));

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.Insertar(new ImagenPropiedad()));
            _repoMock.Verify(r => r.SalvarTodo(), Times.Never);
        }

        [Test]
        public void Actualizar_Valido_RetornaTrueYGuarda()
        {
            // Arrange
            var entidad = new ImagenPropiedad { IdImagenPropiedad = 2, Archivo = "img2.jpg" };
            _repoMock.Setup(r => r.Actualizar(entidad)).Returns(true);
            _repoMock.Setup(r => r.SalvarTodo());

            // Act
            var ok = _useCase.Actualizar(entidad);

            // Assert
            Assert.IsTrue(ok);
            _repoMock.Verify(r => r.Actualizar(entidad), Times.Once);
            _repoMock.Verify(r => r.SalvarTodo(), Times.Once);
        }

        [Test]
        public void Actualizar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.Actualizar(It.IsAny<ImagenPropiedad>()))
                     .Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.Actualizar(new ImagenPropiedad()));
            _repoMock.Verify(r => r.SalvarTodo(), Times.Never);
        }

        [Test]
        public void Eliminar_Valido_RetornaTrueYGuarda()
        {
            // Arrange
            _repoMock.Setup(r => r.Eliminar(10)).Returns(true);
            _repoMock.Setup(r => r.SalvarTodo());

            // Act
            var ok = _useCase.Eliminar(10);

            // Assert
            Assert.IsTrue(ok);
            _repoMock.Verify(r => r.Eliminar(10), Times.Once);
            _repoMock.Verify(r => r.SalvarTodo(), Times.Once);
        }

        [Test]
        public void Eliminar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.Eliminar(It.IsAny<int>()))
                     .Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.Eliminar(99));
            _repoMock.Verify(r => r.SalvarTodo(), Times.Never);
        }

        [Test]
        public void ObtenerPorID_Valido_RetornaEntidad()
        {
            // Arrange
            var entidad = new ImagenPropiedad { IdImagenPropiedad = 5, Archivo = "img5.jpg" };
            _repoMock.Setup(r => r.ObtenerPorID(5)).Returns(entidad);

            // Act
            var result = _useCase.ObtenerPorID(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.IdImagenPropiedad);
            _repoMock.Verify(r => r.ObtenerPorID(5), Times.Once);
        }

        [Test]
        public void ObtenerPorID_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.ObtenerPorID(It.IsAny<int>()))
                     .Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.ObtenerPorID(1));
        }

        #endregion
    }
}
