using Aplicacion.Interfaces;
using Dominio.Interfaces.Repositorio;
using Dominio.Maestras;
using Dominio.Modelos;
using static Dominio.Maestras.MensajesBase;
using Dominio.Excepciones;
using Dominio.Comun;

namespace Aplicacion.UseCase
{

    public class PropiedadesUseCase : IUseCasePropiedad<Propiedad, int>
    {
        #region Atributos
        private readonly IRepositorioPropiedad<Propiedad, int> repositorio;
        #endregion

        #region Constructor
        public PropiedadesUseCase(IRepositorioPropiedad<Propiedad, int> _repositorio)
        {
            repositorio = _repositorio;
        }
        #endregion

        #region Metodos
        public async Task<bool> ActualizarAsync(Propiedad entidad)
        {
            Guard.NoNulo(entidad, "Propiedad");
            Guard.MayorQue(entidad.IdPropiedad, 0, "IdPropiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.MayorQue(entidad.Precio, 0, "Precio");
            Guard.NoNuloOVacio(entidad.CodigoInterno, "CodigoInterno");
            Guard.AnioValido(entidad.Anio, "Anio");
            Guard.MayorQue(entidad.IdPropietario, 0, "IdPropietario");

            try
            {
                var resultado = await repositorio.ActualizarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al actualizar propiedad: {ex.Message}");
            }
        }

        public async Task<bool> EliminarAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdPropiedad");
            
            var existe = await repositorio.ObtenerPorIDAsync(entidadID);
            if (existe == null)
                throw new EntidadNoEncontradaException("Propiedad", entidadID);

            try
            {
                var resultado = await repositorio.EliminarAsync(entidadID);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al eliminar propiedad: {ex.Message}");
            }
        }

        public async Task<Propiedad> InsertarAsync(Propiedad entidad)
        {
            Guard.NoNulo(entidad, "Propiedad");
            Guard.NoNuloOVacio(entidad.Nombre, "Nombre");
            Guard.LongitudMinima(entidad.Nombre, 2, "Nombre");
            Guard.NoNuloOVacio(entidad.Direccion, "Direccion");
            Guard.LongitudMinima(entidad.Direccion, 5, "Direccion");
            Guard.MayorQue(entidad.Precio, 0, "Precio");
            Guard.NoNuloOVacio(entidad.CodigoInterno, "CodigoInterno");
            Guard.AnioValido(entidad.Anio, "Anio");
            Guard.MayorQue(entidad.IdPropietario, 0, "IdPropietario");

            try
            {
                var resultado = await repositorio.InsertarAsync(entidad);
                await repositorio.SalvarTodoAsync();
                return resultado;
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al insertar propiedad: {ex.Message}");
            }
        }

        public async Task<List<Propiedad>> ObtenerPorFiltroAsync(Propiedad entidad, string order)
        {
            Guard.NoNulo(entidad, "Propiedad");
            Guard.NoNuloOVacio(order, "Order");
            
            var ordenesValidas = new[] { "ASC", "DESC" };
            if (!ordenesValidas.Contains(order.ToUpper()))
                throw new ValidacionDominioException("Order", order, "debe ser ASC o DESC");

            try
            {
                return await repositorio.ObtenerPorFiltroAsync(entidad, order);
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al obtener propiedades por filtro: {ex.Message}");
            }
        }

        public async Task<Propiedad> ObtenerPorIDAsync(int entidadID)
        {
            Guard.MayorQue(entidadID, 0, "IdPropiedad");
            
            var resultado = await repositorio.ObtenerPorIDAsync(entidadID);
            
            if (resultado == null)
                throw new EntidadNoEncontradaException("Propiedad", entidadID);
                
            return resultado;
        }

        public async Task<List<Propiedad>> ObtenerTodoAsync()
        {
            try
            {
                return await repositorio.ObtenerTodoAsync();
            }
            catch (Exception ex)
            {
                throw new ValidacionDominioException($"Error al obtener propiedades: {ex.Message}");
            }
        }
        #endregion
    }
}
