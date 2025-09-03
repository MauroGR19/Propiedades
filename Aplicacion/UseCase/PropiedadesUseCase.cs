using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using static Dominio.Maestras.MensajesBase;

namespace Aplicacion.UseCase
{

    public class PropiedadesUseCase : IUseCasePropiedad<Propiedad, int>
    {
        #region Atributos
        private readonly IRepositorioPropiedad<Propiedad, int> repositorio;
        private Excepciones exception = new Excepciones();
        #endregion

        #region Constructor
        public PropiedadesUseCase(IRepositorioPropiedad<Propiedad, int> _repositorio)
        {
            repositorio = _repositorio;
        }
        #endregion

        #region Metodos
        public bool Actualizar(Propiedad entidad)
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

        public Propiedad Insertar(Propiedad entidad)
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

        public List<Propiedad> ObtenerPorFiltro(Propiedad entidad, string order)
        {
            try
            {
                return repositorio.ObtenerPorFiltro(entidad, order);
            }
            catch (Exception ex)
            {
                throw exception.Error(ex, Error.Obtener.ObtenerDeascripcionEnum());
            }
        }

        public Propiedad ObtenerPorID(int entidadID)
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

        public List<Propiedad> ObtenerTodo()
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
