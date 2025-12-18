using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Comun
{
    /// <summary>
    /// DTO de respuesta para consultas paginadas
    /// </summary>
    /// <typeparam name="T">Tipo de entidad en la lista paginada</typeparam>
    /// <example>
    /// {
    ///   "datos": [...],
    ///   "totalRegistros": 150,
    ///   "totalPaginas": 15,
    ///   "paginaActual": 2,
    ///   "tamanioPagina": 10,
    ///   "tienePaginaAnterior": true,
    ///   "tienePaginaSiguiente": true
    /// }
    /// </example>
    public class PaginacionResponse<T>
    {
        /// <summary>
        /// Lista de elementos de la página actual
        /// </summary>
        public IEnumerable<T> Datos { get; set; } = new List<T>();

        /// <summary>
        /// Número total de registros en toda la consulta
        /// </summary>
        /// <example>150</example>
        public int TotalRegistros { get; set; }

        /// <summary>
        /// Número total de páginas disponibles
        /// </summary>
        /// <example>15</example>
        public int TotalPaginas { get; set; }

        /// <summary>
        /// Número de la página actual
        /// </summary>
        /// <example>2</example>
        public int PaginaActual { get; set; }

        /// <summary>
        /// Cantidad de registros por página
        /// </summary>
        /// <example>10</example>
        public int TamanioPagina { get; set; }

        /// <summary>
        /// Indica si existe una página anterior
        /// </summary>
        /// <example>true</example>
        public bool TienePaginaAnterior { get; set; }

        /// <summary>
        /// Indica si existe una página siguiente
        /// </summary>
        /// <example>true</example>
        public bool TienePaginaSiguiente { get; set; }
    }
}
