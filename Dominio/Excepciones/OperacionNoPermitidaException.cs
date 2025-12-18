using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    /// <summary>
    /// Excepción que se lanza cuando se intenta realizar una operación que no está permitida
    /// </summary>
    /// <remarks>
    /// Se utiliza para manejar casos donde:
    /// - Una operación viola reglas de negocio
    /// - El estado actual del objeto no permite la operación
    /// - Restricciones de seguridad o permisos
    /// - Operaciones que requieren condiciones específicas no cumplidas
    /// </remarks>
    /// <example>
    /// throw new OperacionNoPermitidaException("Eliminar propiedad", "tiene historial asociado");
    /// // Resultado: "Operacion 'Eliminar propiedad' no permitida: 'tiene historial asociado'"
    /// </example>
    public class OperacionNoPermitidaException : DominioException
    {
        /// <summary>
        /// Constructor que inicializa la excepción con la operación y la razón por la cual no está permitida
        /// </summary>
        /// <param name="operacion">Nombre de la operación que no se puede realizar</param>
        /// <param name="razon">Razón por la cual la operación no está permitida</param>
        /// <example>
        /// new OperacionNoPermitidaException("Actualizar propiedad", "usuario sin permisos")
        /// // Resultado: "Operacion 'Actualizar propiedad' no permitida: 'usuario sin permisos'"
        /// </example>
        public OperacionNoPermitidaException(string operacion, string razon)
            : base($"Operacion '{operacion}' no permitida: '{razon}'") { }
    }
}
