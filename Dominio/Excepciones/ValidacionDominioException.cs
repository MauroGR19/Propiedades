using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    /// <summary>
    /// Excepción que se lanza cuando fallan las validaciones de reglas de negocio del dominio
    /// </summary>
    /// <remarks>
    /// Se utiliza para manejar errores de validación como:
    /// - Campos requeridos vacíos
    /// - Valores fuera de rango
    /// - Formatos inválidos
    /// - Reglas de negocio no cumplidas
    /// </remarks>
    /// <example>
    /// throw new ValidacionDominioException("Precio", "0", "debe ser mayor a 0");
    /// </example>
    public class ValidacionDominioException : DominioException
    {
        /// <summary>
        /// Constructor que inicializa la excepción con un mensaje genérico
        /// </summary>
        /// <param name="message">Mensaje descriptivo del error de validación</param>
        public ValidacionDominioException (string message) : base(message) { }
        
        /// <summary>
        /// Constructor que inicializa la excepción con información detallada del campo que falló
        /// </summary>
        /// <param name="campo">Nombre del campo que falló la validación</param>
        /// <param name="valor">Valor que causó el fallo</param>
        /// <param name="razon">Razón por la cual falló la validación</param>
        /// <example>
        /// new ValidacionDominioException("Precio", "0", "debe ser mayor a 0")
        /// // Resultado: "Validación fallida en campo 'Precio' con valor '0': debe ser mayor a 0"
        /// </example>
        public ValidacionDominioException (string campo, string valor, string razon)
            : base($"Validación fallida en campo '{campo}' con valor '{valor}': {razon}") { }
    }
}
