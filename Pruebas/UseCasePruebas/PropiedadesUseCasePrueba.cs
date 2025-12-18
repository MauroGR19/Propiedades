using Aplicacion.UseCase;
using Dominio.Interfaces.Repositorio;
using Dominio.Modelos;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pruebas.UseCasePruebas
{
    public class PropiedadesUseCasePrueba
    {
        #region Atributos
        private Mock<IRepositorioPropiedad<Propiedad, int>> _repoMock;
        private Mock<ILogger<PropiedadesUseCase>> _loggerMock;
        private PropiedadesUseCase _useCase;
        #endregion

        #region Constructor
        [SetUp]
        public void Setup()
        {
            _repoMock = new Mock<IRepositorioPropiedad<Propiedad, int>>();
            _loggerMock = new Mock<ILogger<PropiedadesUseCase>>();
            _useCase = new PropiedadesUseCase(_repoMock.Object, _loggerMock.Object);
        }
        #endregion

        #region Metodos

        [Test]
        public async Task Insertar_EntidadValida_RetornaEntidadYGuarda()
        {
            // Arrange
            var entidad = new Propiedad { IdPropiedad = 1, Nombre = "Casa A" };
            _repoMock.Setup(r => r.InsertarAsync(entidad)).ReturnsAsync(entidad);
            _repoMock.Setup(r => r.SalvarTodoAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.InsertarAsync(entidad);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.IdPropiedad);
            _repoMock.Verify(r => r.InsertarAsync(entidad), Times.Once);
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Once);
        }

        [Test]
        public void Insertar_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.InsertarAsync(It.IsAny<Propiedad>()))
                     .ThrowsAsync(new InvalidOperationException("falló repo"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.InsertarAsync(new Propiedad()));
            _repoMock.Verify(r => r.SalvarTodoAsync(), Times.Never);
        }

        [Test]
        public async Task Actualizar_Valido_RetornaTrueYGuarda()
        {
            // Arrange
            var entidad = new Propiedad { IdPropiedad = 2, Nombre = "Casa B" };
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
            _repoMock.Setup(r => r.ActualizarAsync(It.IsAny<Propiedad>()))
                     .ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ActualizarAsync(new Propiedad()));
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
            var entidad = new Propiedad { IdPropiedad = 5, Nombre = "Casa C" };
            _repoMock.Setup(r => r.ObtenerPorIDAsync(5)).ReturnsAsync(entidad);

            // Act
            var result = await _useCase.ObtenerPorIDAsync(5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.IdPropiedad);
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
            var lista = new List<Propiedad>
            {
                new Propiedad { IdPropiedad = 1 },
                new Propiedad { IdPropiedad = 2 }
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
            _repoMock.Setup(r => r.ObtenerTodoAsync())
                     .ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ObtenerTodoAsync());
        }

        [Test]
        public async Task ObtenerPorFiltro_Valido_RetornaLista()
        {
            // Arrange
            var filtro = new Propiedad { Direccion = "Calle", Precio = 100, Anio = 2024 };
            var lista = new List<Propiedad> { new Propiedad { IdPropiedad = 3 } };

            _repoMock.Setup(r => r.ObtenerPorFiltroAsync(filtro, "ASC")).ReturnsAsync(lista);

            // Act
            var result = await _useCase.ObtenerPorFiltroAsync(filtro, "ASC");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            _repoMock.Verify(r => r.ObtenerPorFiltroAsync(filtro, "ASC"), Times.Once);
        }

        [Test]
        public void ObtenerPorFiltro_RepositorioFalla_LanzaExcepcion()
        {
            // Arrange
            _repoMock.Setup(r => r.ObtenerPorFiltroAsync(It.IsAny<Propiedad>(), It.IsAny<string>()))
                     .ThrowsAsync(new InvalidOperationException());

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _useCase.ObtenerPorFiltroAsync(new Propiedad(), "DESC"));
        }

        #endregion
    }
}