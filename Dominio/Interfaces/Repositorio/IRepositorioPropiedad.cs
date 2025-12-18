using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Repositorio
{
    public interface IRepositorioPropiedad<TEntidad, TEntidadID> : IRepositorioBase<TEntidad, TEntidadID>
    {
        Task<List<TEntidad>> ObtenerPorFiltroAsync(TEntidad entidad, string order);
    }
}
