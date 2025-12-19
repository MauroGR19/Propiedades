using Aplicacion.Servicios.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios.Servicio
{
    public class ServicioCache : IServicioCache
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<ServicioCache> _logger;

        public ServicioCache(IMemoryCache cache, ILogger<ServicioCache> logger)
        {
            _cache = cache;
            _logger = logger;
        }   

        public Task EstablecerAsync<T>(string clave, T valor, TimeSpan expiracion) where T : class
        {
            _cache.Set(clave, valor, expiracion);
            _logger.LogInformation("Cache ESTABLECIDO para clave: {Clave}, expira en: {Expiracion}", clave, expiracion);
            return Task.CompletedTask;
        }

        public Task<T?> ObtenerAsync<T>(string clave) where T : class
        {
            var resultado = _cache.Get<T>(clave);
            if (resultado != null)
                _logger.LogInformation("Cache ACIERTO para clave: { Clave}", clave);
            else
                _logger.LogInformation("Cache FALLO para clave: { Clave}", clave);
            return Task.FromResult(resultado);
        }

        public Task RemoverAsync(string clave)
        {
            _cache.Remove(clave);
            _logger.LogInformation("Cache REMOVIDO para clave: { Clave}", clave);
            return Task.CompletedTask;
        }

        public Task RemoverPorPatronAsync(string patron)
        {
            _logger.LogInformation("Cache REMOVIDO para patron: { Patron}", patron);
            return Task.CompletedTask;
        }
    }
}
