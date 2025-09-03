using Aplicacion.UseCase;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Moq;

namespace Pruebas.UseCasePruebas
{
    public class PropiedadesUseCasePrueba
    {
        #region Atributos
        private Mock<IRepositorioPropiedad<Propiedad, int>> _repoMock;
        private PropiedadesUseCase _useCase;
        #endregion

        #region Constructor
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IRepositorioPropiedad<Propiedad, int>>();
            _useCase = new PropiedadesUseCase(_repoMock.Object);
        }
        #endregion

        #region Metodos

        [Test]
        public void Insertar_EntidadValida_RetornaEntidadYGuarda()
        {
            // Arrange
            var entidad = new Propiedad { IdPropiedad = 1, Nombre = "Casa A" };
            _repoMock.Setup(r => r.Insertar(entidad)).Returns(entidad);
            _repoMock.Setup(r => r.SalvarTodo());

            // Act
            var result = _useCase.Insertar(entidad);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.IdPropiedad);
            _repoMock.Verify(r => r.Insertar(entidad), Times.Once);
            _repoMock.Verify(r => r.SalvarTodo(), Times.Once);
        }

        [Test]
        public void Insertar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.Insertar(It.IsAny<Propiedad>()))
                     .Throws(new InvalidOperationException("falló repo"));

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.Insertar(new Propiedad()));
            _repoMock.Verify(r => r.SalvarTodo(), Times.Never);
        }

        [Test]
        public void Actualizar_Valido_RetornaTrueYGuarda()
        {
            // Arrange
            var entidad = new Propiedad { IdPropiedad = 2, Nombre = "Casa B" };
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
            _repoMock.Setup(r => r.Actualizar(It.IsAny<Propiedad>()))
                     .Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.Actualizar(new Propiedad()));
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
            var entidad = new Propiedad { IdPropiedad = 5, Nombre = "Casa C" };
            _repoMock.Setup(r => r.ObtenerPorID(5)).Returns(entidad);

            // Act
            var result = _useCase.ObtenerPorID(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.IdPropiedad);
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

        [Test]
        public void ObtenerTodo_SinFiltros_RetornaLista()
        {
            // Arrange
            var lista = new List<Propiedad>
            {
                new Propiedad { IdPropiedad = 1 },
                new Propiedad { IdPropiedad = 2 }
            };
            _repoMock.Setup(r => r.ObtenerTodo()).Returns(lista);

            // Act
            var result = _useCase.ObtenerTodo();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            _repoMock.Verify(r => r.ObtenerTodo(), Times.Once);
        }

        [Test]
        public void ObtenerTodo_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.ObtenerTodo())
                     .Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.ObtenerTodo());
        }

        [Test]
        public void ObtenerPorFiltro_Valido_RetornaLista()
        {
            // Arrange
            var filtro = new Propiedad { Direccion = "Calle", Precio = 100, Anio = 2024 };
            var lista = new List<Propiedad> { new Propiedad { IdPropiedad = 3 } };

            _repoMock.Setup(r => r.ObtenerPorFiltro(filtro, "ASC")).Returns(lista);

            // Act
            var result = _useCase.ObtenerPorFiltro(filtro, "ASC");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            _repoMock.Verify(r => r.ObtenerPorFiltro(filtro, "ASC"), Times.Once);
        }

        [Test]
        public void ObtenerPorFiltro_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.ObtenerPorFiltro(It.IsAny<Propiedad>(), It.IsAny<string>()))
                     .Throws(new InvalidOperationException());

            // Act & Assert
            Assert.Throws<Exception>(() => _useCase.ObtenerPorFiltro(new Propiedad(), "DESC"));
        }

        #endregion
    }
}
