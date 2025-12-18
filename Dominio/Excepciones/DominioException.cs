using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    /// <summary>
    /// Clase base abstracta para todas las excepciones del dominio
    /// </summary>
    /// <remarks>
    /// Proporciona una jerarquía común para todas las excepciones relacionadas
    /// con reglas de negocio y lógica del dominio. Permite un manejo centralizado
    /// de errores específicos del negocio en la aplicación.
    /// </remarks>
    public abstract class DominioException : Exception
    {
        /// <summary>
        /// Constructor que inicializa la excepción con un mensaje
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error</param>
        protected DominioException(string message) : base(message) { }
        
        /// <summary>
        /// Constructor que inicializa la excepción con un mensaje y una excepción interna
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error</param>
        /// <param name="InnerException">Excepción que causó este error</param>
        protected DominioException(string message, Exception InnerException) : base(message, InnerException) { }
    }
}
   