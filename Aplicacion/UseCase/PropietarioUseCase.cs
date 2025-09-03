using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using static Dominio.Maestras.MensajesBase;

namespace Aplicacion.UseCase
{
    public class PropietarioUseCase : IUseCaseBase<Propietario, int>
    {
        #region Atributos
        private readonly IRepositorioBase<Propietario, int> repositorio;
        private Excepciones exception = new Excepciones();
        #endregion

        #region Constructor
        public PropietarioUseCase(IRepositorioBase<Propietario, int> _repositorio)
        {
            repositorio = _repositorio;
        }
        #endregion

        #region Metodos
        public bool Actualizar(Propietario entidad)
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

        public Propietario Insertar(Propietario entidad)
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

        public Propietario ObtenerPorID(int entidadID)
        {
            try
            {
                return repositorio.ObtenerPorID(entidadID);
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, Error.Obtener.ObtenerDeascripcionEnum());
            }
            ;
        }

        public List<Propietario> ObtenerTodo()
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
