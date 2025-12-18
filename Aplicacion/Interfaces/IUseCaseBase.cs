using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Comun;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// Interfaz base que define las operaciones CRUD comunes para todos los casos de uso
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad de dominio</typeparam>
    /// <typeparam name="TEntidadID">Tipo del identificador de la entidad</typeparam>
    public interface IUseCaseBase<TEntidad, TEntidadID>
       : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>, IListar<TEntidad, TEntidadID>
    {
        /// <summary>
        /// Obtiene una lista paginada de entidades
        /// </summary>
        /// <param name="parametros">Parámetros de paginación (página, tamaño, etc.)</param>
        /// <returns>Resultado paginado con las entidades y metadatos de paginación</returns>
        Task<ResultadoPaginado<TEntidad>> ObtenerPaginadoAsync(PaginacionParametros parametros);
    }
}
