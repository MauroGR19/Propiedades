using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Comun
{
    /// <summary>
    /// Estructura estándar de respuesta para todos los endpoints de la API
    /// </summary>
    /// <typeparam name="T">Tipo de datos que contiene la respuesta</typeparam>
    /// <example>
    /// {
    ///   "exitoso": true,
    ///   "mensaje": "Operación exitosa",
    ///   "datos": { ... },
    ///   "errores": []
    /// }
    /// </example>
    public class RespuestaApi<T>
    {
        /// <summary>
        /// Indica si la operación fue exitosa
        /// </summary>
        /// <example>true</example>
        public bool Exitoso { get; set; }

        /// <summary>
        /// Mensaje descriptivo del resultado de la operación
        /// </summary>
        /// <example>Operación exitosa</example>
        public string Mensaje { get; set; }

        /// <summary>
        /// Datos de respuesta de la operación
        /// </summary>
        public T Datos { get; set; }

        /// <summary>
        /// Lista de errores en caso de que la operación falle
        /// </summary>
        /// <example>["Error de validación", "Campo requerido"]</example>
        public List<string> Errores { get; set; }

        /// <summary>
        /// Constructor por defecto que inicializa la lista de errores
        /// </summary>
        public RespuestaApi()
        {
            Errores = new List<string>();
        }

        /// <summary>
        /// Crea una respuesta exitosa con datos y mensaje
        /// </summary>
        /// <param name="datos">Datos a incluir en la respuesta</param>
        /// <param name="mensaje">Mensaje descriptivo del éxito</param>
        /// <returns>Respuesta API marcada como exitosa</returns>
        public static RespuestaApi<T> RespuestaExitosa(T datos, string mensaje = "Operación exitosa")
        {
            return new RespuestaApi<T>
            {
                Exitoso = true,
                Mensaje = mensaje,
                Datos = datos
            };
        }

        /// <summary>
        /// Crea una respuesta de error con mensaje y lista de errores
        /// </summary>
        /// <param name="mensaje">Mensaje descriptivo del error</param>
        /// <param name="errores">Lista opcional de errores detallados</param>
        /// <returns>Respuesta API marcada como fallida</returns>
        public static RespuestaApi<T> RespuestaError(string mensaje, List<string> errores = null)
        {
            return new RespuestaApi<T>
            {
                Exitoso = false,
                Mensaje = mensaje,
                Errores = errores ?? new List<string>()
            };
        }
    }
}
