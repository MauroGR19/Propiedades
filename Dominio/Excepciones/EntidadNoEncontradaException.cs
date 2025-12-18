using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Excepciones
{
    /// <summary>
    /// Excepción que se lanza cuando no se encuentra una entidad solicitada en el sistema
    /// </summary>
    /// <remarks>
    /// Se utiliza cuando se intenta acceder a una entidad que no existe en la base de datos
    /// o en el contexto actual. Proporciona información específica sobre qué entidad
    /// y qué identificador se estaba buscando.
    /// </remarks>
    /// <example>
    /// throw new EntidadNoEncontradaException("Propiedad", 123);
    /// // Resultado: "No se encontró la entidad Propiedad con id 123"
    /// </example>
    public class EntidadNoEncontradaException : DominioException
    {
        /// <summary>
        /// Constructor que inicializa la excepción con el nombre de la entidad y su identificador
        /// </summary>
        /// <param name="entidad">Nombre de la entidad que no se encontró</param>
        /// <param name="id">Identificador de la entidad que se estaba buscando</param>
        /// <example>
        /// new EntidadNoEncontradaException("Propietario", 456)
        /// // Resultado: "No se encontró la entidad Propietario con id 456"
        /// </example>
        public EntidadNoEncontradaException (string entidad, object id) 
            : base($"No se encontró la entidad {entidad} con id {id}") { }
    }
}
