using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    /// <summary>
    /// Excepción que se lanza cuando ocurren errores relacionados con la autenticación de usuarios
    /// </summary>
    /// <remarks>
    /// Se utiliza para manejar errores como:
    /// - Credenciales inválidas
    /// - Usuario no encontrado
    /// - Contraseña incorrecta
    /// - Errores en la creación de usuarios
    /// </remarks>
    /// <example>
    /// throw new AutenticacionException("Usuario o contraseña incorrectos");
    /// </example>
    public class AutenticacionException : DominioException
    { 
        /// <summary>
        /// Constructor que inicializa la excepción con un mensaje de error de autenticación
        /// </summary>
        /// <param name="mensaje">Mensaje descriptivo del error de autenticación</param>
        public AutenticacionException(string mensaje) : base(mensaje) { } 
    }
}
