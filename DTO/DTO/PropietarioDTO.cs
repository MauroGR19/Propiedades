namespace DTO.DTO
{
    /// <summary>
    /// DTO para propietarios de inmuebles
    /// </summary>
    public class PropietarioDTO
    {
        /// <summary>
        /// Número de documento de identidad del propietario
        /// </summary>
        /// <example>12345678</example>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Tipo de documento de identidad
        /// </summary>
        /// <example>CC</example>
        public string TipoDocumento { get; set; }

        /// <summary>
        /// Nombre completo del propietario
        /// </summary>
        /// <example>Juan Pérez García</example>
        public string Nombre { get; set; }

        /// <summary>
        /// Dirección de residencia del propietario
        /// </summary>
        /// <example>Carrera 15 #32-41, Medellín</example>
        public string Direccion { get; set; }

        /// <summary>
        /// URL o ruta de la foto del propietario
        /// </summary>
        /// <example>https://ejemplo.com/foto.jpg</example>
        public string Foto { get; set; }

        /// <summary>
        /// Fecha de nacimiento del propietario
        /// </summary>
        /// <example>1985-03-15</example>
        public DateTime FechaNacimiento { get; set; }
    }
}
