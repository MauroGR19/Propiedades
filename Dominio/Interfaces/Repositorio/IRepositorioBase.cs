using Dominio.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Repositorio
{
    public interface IRepositorioBase<TEntidad, TEntidadID>
    : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>, IListar<TEntidad, TEntidadID>, ISalvarTodo
    {
        Task<ResultadoPaginado<TEntidad>> ObtenerPaginadoAsync(PaginacionParametros parametros);
    }
}
