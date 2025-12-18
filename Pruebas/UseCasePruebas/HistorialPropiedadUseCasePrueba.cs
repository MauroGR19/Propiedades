using Aplicacion.UseCase;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pruebas.UseCasePruebas
{
    public class HistorialPropiedadUseCasePrueba
    {
        #region Atributos
        private Mock<IRepositorioBase<HistorialPropiedad, int>> _repoMock;
        private HistorialPropiedadUseCase _useCase;
        #endregion

        #region Constructor
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IRepositorioBase<HistorialPropiedad, int>>();
            _useCase = new HistorialPropiedadUseCase(_repoMock.Object);
        }
        #endregion

        #region Metodos

        [Test]
        public async Task Insertar_EntidadValida_RetornaEntidadYGuarda()
        {
            // Arrange
            var entidad = new HistorialPropiedad { IdHistorialPropiedad = 1, Nombre = "Venta A" };
            _repoMock.Setup(r => r.InsertarAsync(entidad)).ReturnsAsync(entidad);
            _repoMock.Setup(r => r.SalvarTodoAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.InsertarAsync(entidad);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.IdHistorialPropiedad);
            _repoMock.Verify(r => r.InsertarAsync(entidad), Times.Once);
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Once);
        }

        [Test]
        public void Insertar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            var entidad = new HistorialPropiedad { IdHistorialPropiedad = 1 };
            _repoMock.Setup(r => r.InsertarAsync(It.IsAny<HistorialPropiedad>()))
                     .ThrowsAsync(new InvalidOperationException("falló repo"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.InsertarAsync(entidad));
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Never);
        }

        [Test]
        public async Task Actualizar_Valido_RetornaTrueYGuarda()
        {
            // Arrange
            var entidad = new HistorialPropiedad { IdHistorialPropiedad = 2, Nombre = "Actualizada" };
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
            _repoMock.Setup(r => r.ActualizarAsync(It.IsAny<HistorialPropiedad>()))
                     .ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ActualizarAsync(new HistorialPropiedad()));
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
            var entidad = new HistorialPropiedad { IdHistorialPropiedad = 5, Nombre = "Historial X" };
            _repoMock.Setup(r => r.ObtenerPorIDAsync(5)).ReturnsAsync(entidad);

            // Act
            var result = await _useCase.ObtenerPorIDAsync(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.IdHistorialPropiedad);
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

        [Test]
        public async Task ObtenerTodo_SinFiltros_RetornaLista()
        {
            // Arrange
            var lista = new List<HistorialPropiedad>
            {
                new HistorialPropiedad { IdHistorialPropiedad = 1 },
                new HistorialPropiedad { IdHistorialPropiedad = 2 }
            };
            _repoMock.Setup(r => r.ObtenerTodoAsync()).ReturnsAsync(lista);

            // Act
            var result = await _useCase.ObtenerTodoAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            _repoMock.Verify(r => r.ObtenerTodoAsync(), Times.Once);
        }

        [Test]
        public void ObtenerTodo_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.ObtenerTodoAsync()).ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ObtenerTodoAsync());
        }

        #endregion
    }
}