using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using static Dominio.Maestras.MensajesBase;
using Dominio.Excepciones;
using Dominio.Comun;

namespace Aplicacion.UseCase
{
    public class PropietarioUseCase : IUseCaseBase<Propietario, int>
    {
        #region Atributos
        private readonly IRepositorioBase<Propietario, int> repositorio;
        #endregion

        #region Constructor
        public PropietarioUseCase(IRepositorioBase<Propietario, int> _repositorio)
        {
            repositorio = _repositorio;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(Propietario entidad)
        {
            Guard.NoNulo(entidad, "Propietario");
            Guard.MayorQue(entidad.IdPropietario, 0, "IdPropietario");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.FechaNoFutura(entidad.FechaNacimiento, "FechaNacimiento");
            Guard.FechaNoMuyAntigua(entidad.FechaNacimiento, 120, "FechaNacimiento");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al actualizar propietario: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdPropietario");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("Propietario", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al eliminar propietario: {ex.Message}");
            }
        }

        public async Task<Propietario> InsertarAsync(Propietario entidad)
        {
            Guard.NoNulo(entidad, "Propietario");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.FechaNoFutura(entidad.FechaNacimiento, "FechaNacimiento");
            Guard.FechaNoMuyAntigua(entidad.FechaNacimiento, 120, "FechaNacimiento");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al insertar propietario: {ex.Message}");
            }
        }

        public async Task<Propietario> ObtenerPorIDAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdPropietario");
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("Propietario", entidadID);
                
            return resultado;
        }

        public async Task<List<Propietario>> ObtenerTodoAsync()
        {
            try
            {
                return await repositorio.ObtenerTodoAsync();
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al obtener propietarios: {ex.Message}");
            }
        }
        #endregion
    }
}
