using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Comun
{
    /// <summary>
    /// DTO para parámetros de paginación en consultas
    /// </summary>
    /// <example>
    /// GET /api/propiedades/paginado?pagina=2&tamanioPagina=20
    /// </example>
    public class PaginacionRequest
    {
        /// <summary>
        /// Número de página a obtener (inicia en 1)
        /// </summary>
        /// <example>1</example>
        public int Pagina { get; set; } = 1;

        /// <summary>
        /// Cantidad de registros por página (máximo 100)
        /// </summary>
        /// <example>10</example>
        public int TamanioPagina { get; set; } = 10;
    }
}
