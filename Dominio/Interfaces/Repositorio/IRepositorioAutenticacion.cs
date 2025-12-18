using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Repositorio
{
    public interface IRepositorioAutenticacion<TEntidad, TEntidadID>
        : IInsertar<TEntidad>, ISalvarTodo
    {
        Task<TEntidad> ObtenerPorUsuarioAsync(TEntidadID Usuario);
    }
}
