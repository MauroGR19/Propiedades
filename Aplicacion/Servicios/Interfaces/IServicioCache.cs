using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.Interfaces
{
    public interface IServicioCache
    {
        Task<T?> ObtenerAsync<T>(string clave) where T : class;
        Task EstablecerAsync<T>(string clave, T valor, TimeSpan expiracion) where T : class;
        Task RemoverAsync(string clave);
        Task RemoverPorPatronAsync(string patron);
    }
}
