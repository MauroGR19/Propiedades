using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Comun
{
    /// <summary>
    /// DTO que contiene información detallada sobre errores ocurridos en la API
    /// </summary>
    /// <remarks>
    /// Utilizado por el middleware de manejo de excepciones para proporcionar
    /// información detallada sobre errores a los clientes de la API
    /// </remarks>
    /// <example>
    /// {
    ///   "codigoEstado": 400,
    ///   "mensaje": "Error de validación",
    ///   "tipo": "ValidationException",
    ///   "detalles": "El campo nombre es requerido",
    ///   "fechaHora": "2024-01-15T10:30:00Z",
    ///   "ruta": "/api/propiedades"
    /// }
    /// </example>
    public class DetallesError
    {
        /// <summary>
        /// Código de estado HTTP del error
        /// </summary>
        /// <example>400</example>
        public int CodigoEstado { get; set; }

        /// <summary>
        /// Mensaje descriptivo del error
        /// </summary>
        /// <example>Error de validación</example>
        public string Mensaje { get; set; }

        /// <summary>
        /// Tipo de excepción que causó el error
        /// </summary>
        /// <example>ValidationException</example>
        public string Tipo { get; set; }

        /// <summary>
        /// Detalles adicionales sobre el error
        /// </summary>
        /// <example>El campo nombre es requerido</example>
        public string Detalles { get; set; }

        /// <summary>
        /// Fecha y hora UTC cuando ocurrió el error
        /// </summary>
        /// <example>2024-01-15T10:30:00Z</example>
        public DateTime FechaHora { get; set; }

        /// <summary>
        /// Ruta del endpoint donde ocurrió el error
        /// </summary>
        /// <example>/api/propiedades</example>
        public string Ruta { get; set; }

        /// <summary>
        /// Constructor que inicializa la fecha y hora actual en UTC
        /// </summary>
        public DetallesError()
        {
            FechaHora = DateTime.UtcNow;
        }
    }
}
