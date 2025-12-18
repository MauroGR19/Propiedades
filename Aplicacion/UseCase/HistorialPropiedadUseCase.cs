using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using static Dominio.Maestras.MensajesBase;
using Dominio.Excepciones;
using Dominio.Comun;

namespace Aplicacion.UseCase
{
    public class HistorialPropiedadUseCase : IUseCaseBase<HistorialPropiedad, int>
    {
        #region Atributos
        private readonly IRepositorioBase<HistorialPropiedad, int> repositorio;
        #endregion

        #region Constructor
        public HistorialPropiedadUseCase(IRepositorioBase<HistorialPropiedad, int> _repositorio)
        {
            repositorio = _repositorio;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(HistorialPropiedad entidad)
        {
            Guard.NoNulo(entidad, "HistorialPropiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.MayorQue(entidad.Valor, 0, "Valor");
            Guard.MayorOIgualQue(entidad.Impuesto, 0, "Impuesto");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");
            Guard.FechaNoFutura(entidad.FechaVenta, "FechaVenta");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al actualizar historial: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdHistorialPropiedad");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("HistorialPropiedad", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al eliminar historial: {ex.Message}");
            }
        }

        public async Task<HistorialPropiedad> InsertarAsync(HistorialPropiedad entidad)
        {
            Guard.NoNulo(entidad, "HistorialPropiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.MayorQue(entidad.Valor, 0, "Valor");
            Guard.MayorOIgualQue(entidad.Impuesto, 0, "Impuesto");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");
            Guard.FechaNoFutura(entidad.FechaVenta, "FechaVenta");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al insertar historial: {ex.Message}");
            }
        }

        public async Task<HistorialPropiedad> ObtenerPorIDAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdHistorialPropiedad");
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("HistorialPropiedad", entidadID);
                
            return resultado;
        }

        public async Task<List<HistorialPropiedad>> ObtenerTodoAsync()
        {
            try
            {
                return await repositorio.ObtenerTodoAsync();
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al obtener historiales: {ex.Message}");
            }
        }
        #endregion

    }
}
