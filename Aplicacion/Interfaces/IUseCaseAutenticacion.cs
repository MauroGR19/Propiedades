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
        Task<TEntidad> ObtenerAutenticacionAsync(TEntidadID Usuario, TEntidadID Contrasena);

        string Token(TEntidadID Usuario);
    }
}
