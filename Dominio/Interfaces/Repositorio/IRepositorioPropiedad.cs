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
        List<TEntidad> ObtenerTodo(string order);

        List<TEntidad> ObtenerPorFiltro(TEntidad entidad, string order);

        TEntidad ObtenerPorID(TEntidadID entidadID, string codTarea, DateTime fechaInicio);
    }
}
