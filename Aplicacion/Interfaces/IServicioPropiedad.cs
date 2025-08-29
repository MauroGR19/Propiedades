using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IServicioPropiedad<TEntidad, TEntidadID>
    : IInsertar<TEntidad>, IActualizar<TEntidad>, IEliminar<TEntidadID>
    {
        List<TEntidad> ObtenerTodo(string order);

        List<TEntidad> ObtenerPorFiltro(TEntidad entidad, string order);

        TEntidad ObtenerPorID(TEntidadID entidadID, string codTarea, DateTime fechaInicio);
    }
}
