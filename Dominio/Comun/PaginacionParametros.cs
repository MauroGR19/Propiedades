using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comun
{
    public class PaginacionParametros
    {
        public int Pagina { get; set; } = 1;
        public int TamanioPagina { get; set; } = 10;
    }
}
