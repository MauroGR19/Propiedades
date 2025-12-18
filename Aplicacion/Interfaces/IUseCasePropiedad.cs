using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IUseCasePropiedad<TEntidad, TEntidadID>
    : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>
    {
        Task<List<TEntidad>> ObtenerTodoAsync();

        Task<List<TEntidad>> ObtenerPorFiltroAsync(TEntidad entidad, string order);

        Task<TEntidad> ObtenerPorIDAsync(TEntidadID entidadID);
    }
}
