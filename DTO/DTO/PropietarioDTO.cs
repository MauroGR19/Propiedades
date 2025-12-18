namespace DTO.DTO
{
    /// <summary>
    /// DTO para propietarios de inmuebles
    /// </summary>
    public class PropietarioDTO
    {
        /// <summary>
        /// Identificador único del propietario
        /// </summary>
        /// <example>1</example>
        public int IdPropietario { get; set; }

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
