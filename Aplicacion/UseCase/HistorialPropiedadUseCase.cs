using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using static Dominio.Maestras.MensajesBase;

namespace Aplicacion.UseCase
{
    public class HistorialPropiedadUseCase : IUseCaseBase<HistorialPropiedad, int>
    {
        #region Atributos
        private readonly IRepositorioBase<HistorialPropiedad, int> repositorio;
        private Excepciones exception = new Excepciones();
        #endregion

        #region Constructor
        public HistorialPropiedadUseCase(IRepositorioBase<HistorialPropiedad, int> _repositorio)
        {
            repositorio = _repositorio;
        }
        #endregion

        #region Metodos
        public bool Actualizar(HistorialPropiedad entidad)
        {
            try
            {
                var resultado = repositorio.Actualizar(entidad);
                repositorio.SalvarTodo();
                return resultado;
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, Error.Actualizar.ObtenerDeascripcionEnum());
            }
        }

        public bool Eliminar(int entidadID)
        {
            try
            {
                var resultado = repositorio.Eliminar(entidadID);
                repositorio.SalvarTodo();
                return resultado;
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, Error.Eliminar.ObtenerDeascripcionEnum());
            }
        }

        public HistorialPropiedad Insertar(HistorialPropiedad entidad)
        {
            try
            {
                var resultado = repositorio.Insertar(entidad);
                repositorio.SalvarTodo();
                return resultado;
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, Error.Insertar.ObtenerDeascripcionEnum());
            }
        }

        public HistorialPropiedad ObtenerPorID(int entidadID)
        {
            try
            {
                return repositorio.ObtenerPorID(entidadID);
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, Error.Obtener.ObtenerDeascripcionEnum());
            }
        }

        public List<HistorialPropiedad> ObtenerTodo()
        {
            try
            {
                return repositorio.ObtenerTodo();
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, Error.Obtener.ObtenerDeascripcionEnum());
            }
        }
        #endregion

    }
}
