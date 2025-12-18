using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IEliminar<TEntidadID>
    {
       Task<bool> EliminarAsync(TEntidadID entidadID);
    }
}
