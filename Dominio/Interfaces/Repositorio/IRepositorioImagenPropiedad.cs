using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Repositorio
{
    public interface IRepositorioImagenPropiedad<TEntidad, TEntidadID>
    : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>, ISalvarTodo
    {
        Task<TEntidad> ObtenerPorIDAsync(TEntidadID entidadID);
    }
}
