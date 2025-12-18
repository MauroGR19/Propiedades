using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    /// <summary>
    /// Interfaz que define las operaciones de autenticación y gestión de usuarios
    /// </summary>
    /// <typeparam name="TEntidad">Tipo de la entidad de autenticación</typeparam>
    /// <typeparam name="TEntidadID">Tipo del identificador (generalmente string para usuario)</typeparam>
    public interface IUseCaseAutenticacion<TEntidad, TEntidadID>
    : IInsertar<TEntidad>
    {
        /// <summary>
        /// Autentica un usuario con sus credenciales
        /// </summary>
        /// <param name="Usuario">Nombre de usuario</param>
        /// <param name="Contrasena">Contraseña del usuario</param>
        /// <returns>Entidad de autenticación si las credenciales son válidas, null en caso contrario</returns>
        Task<TEntidad> ObtenerAutenticacionAsync(TEntidadID Usuario, TEntidadID Contrasena);

        /// <summary>
        /// Genera un token JWT para el usuario autenticado
        /// </summary>
        /// <param name="Usuario">Nombre de usuario para el cual generar el token</param>
        /// <returns>Token JWT válido por 1 hora</returns>
        string Token(TEntidadID Usuario);
    }
}
