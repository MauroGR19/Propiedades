using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Repositorio
{
    public interface IRepositorioPropiedad<TEntidad, TEntidadID>
    : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>, ISalvarTodo
    {
        Task<List<TEntidad>> ObtenerTodoAsync();

        Task<List<TEntidad>> ObtenerPorFiltroAsync(TEntidad entidad, string order);

        Task<TEntidad> ObtenerPorIDAsync(TEntidadID entidadID);
    }
}
