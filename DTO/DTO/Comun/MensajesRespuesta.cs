using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Comun
{
    /// <summary>
    /// Clase estática que proporciona mensajes estandarizados para las respuestas de la API
    /// </summary>
    /// <remarks>
    /// Centraliza todos los mensajes de respuesta para mantener consistencia
    /// en la comunicación con los clientes de la API
    /// </remarks>
    public class MensajesRespuesta
    {
        /// <summary>
        /// Mensaje para operaciones de inserción exitosas
        /// </summary>
        /// <param name="entidad">Nombre de la entidad insertada</param>
        /// <returns>Mensaje formateado de inserción exitosa</returns>
        public static string Insertado(string entidad) => $"{entidad} insertado exitosamente";

        /// <summary>
        /// Mensaje para operaciones de actualización exitosas
        /// </summary>
        /// <param name="entidad">Nombre de la entidad actualizada</param>
        /// <returns>Mensaje formateado de actualización exitosa</returns>
        public static string Actualizado(string entidad) => $"{entidad} actualizado exitosamente";

        /// <summary>
        /// Mensaje para operaciones de eliminación exitosas
        /// </summary>
        /// <param name="entidad">Nombre de la entidad eliminada</param>
        /// <returns>Mensaje formateado de eliminación exitosa</returns>
        public static string Eliminado(string entidad) => $"{entidad} eliminado exitosamente";

        /// <summary>
        /// Mensaje para consultas individuales exitosas
        /// </summary>
        /// <param name="entidad">Nombre de la entidad obtenida</param>
        /// <returns>Mensaje formateado de consulta exitosa</returns>
        public static string Obtenido(string entidad) => $"{entidad} obtenido exitosamente";

        /// <summary>
        /// Mensaje para consultas de listas exitosas
        /// </summary>
        /// <param name="entidad">Nombre de la entidad consultada</param>
        /// <param name="cantidad">Cantidad de registros obtenidos</param>
        /// <returns>Mensaje formateado con la cantidad de registros</returns>
        public static string ObtenidoLista(string entidad, int cantidad) => $"Se obtuvieron {cantidad} {entidad.ToLower()}";

        /// <summary>
        /// Mensaje para cuando no se encuentra una entidad
        /// </summary>
        /// <param name="entidad">Nombre de la entidad no encontrada</param>
        /// <returns>Mensaje formateado de entidad no encontrada</returns>
        public static string NoEncontrado(string entidad) => $"{entidad} no encontrado";

        /// <summary>
        /// Mensaje genérico para operaciones exitosas
        /// </summary>
        /// <returns>Mensaje de operación exitosa</returns>
        public static string OperacionExitosa() => "Operación exitosa";

        /// <summary>
        /// Mensaje para errores de validación de datos
        /// </summary>
        /// <returns>Mensaje de error de validación</returns>
        public static string ErrorValidacion() => "Error en la validación de datos";

        /// <summary>
        /// Mensaje para errores internos del servidor
        /// </summary>
        /// <returns>Mensaje de error interno</returns>
        public static string ErrorInterno() => "Error interno del servidor";

        /// <summary>
        /// Mensaje para accesos no autorizados
        /// </summary>
        /// <returns>Mensaje de no autorizado</returns>
        public static string NoAutorizado() => "No autorizado";

        /// <summary>
        /// Mensaje para generación exitosa de tokens JWT
        /// </summary>
        /// <returns>Mensaje de token generado</returns>
        public static string TokenGenerado() => "Token generado exitosamente";

        /// <summary>
        /// Mensaje para fallos de autenticación
        /// </summary>
        /// <returns>Mensaje de credenciales incorrectas</returns>
        public static string AutenticacionFallida() => "Usuario o contraseña incorrectos";
    }
}
