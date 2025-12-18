using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones específicas para la gestión de propiedades inmobiliarias
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad Propiedad</typeparam>
    /// <typeparam name="TEntidadID">Tipo del identificador de la propiedad (int)</typeparam>
    public interface IUseCasePropiedad<TEntidad, TEntidadID>: IUseCaseBase<TEntidad, TEntidadID>
    {
        /// <summary>
        /// Obtiene propiedades aplicando filtros de búsqueda
        /// </summary>
        /// <param name="entidad">Entidad con los criterios de filtrado (dirección, precio, año, etc.)</param>
        /// <param name="order">Orden de los resultados (ASC o DESC)</param>
        /// <returns>Lista de propiedades que cumplen con los criterios de filtrado</returns>
        Task<List<TEntidad>> ObtenerPorFiltroAsync(TEntidad entidad, string order);
    }
}
