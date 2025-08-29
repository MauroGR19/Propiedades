using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IListar<TEntidad, TEntidadID>
    {
        List<TEntidad> ObtenerTodo();

        TEntidad ObtenerPorID(TEntidadID entidadID);
    }
}
