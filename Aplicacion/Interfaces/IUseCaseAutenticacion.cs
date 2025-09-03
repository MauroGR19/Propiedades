using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IUseCaseAutenticacion<TEntidad, TEntidadID>
    : IInsertar<TEntidad>
    {
        TEntidad ObtenerAutenticacion(TEntidadID Usuario, TEntidadID Contrasena);

        string Token(TEntidadID Usuario);
    }
}
