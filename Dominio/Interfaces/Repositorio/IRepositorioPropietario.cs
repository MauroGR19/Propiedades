using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Modelos;

namespace Dominio.Interfaces.Repositorio
{
    public interface IRepositorioPropietario<TEntidad, TEntidadID> : IRepositorioBase<TEntidad, TEntidadID>
    {
    }
}
