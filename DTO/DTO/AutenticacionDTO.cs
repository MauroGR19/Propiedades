namespace DTO.DTO
{
    /// <summary>
    /// DTO para autenticación de usuarios
    /// </summary>
    public class AutenticacionDTO
    {
        /// <summary>
        /// Nombre de usuario único en el sistema
        /// </summary>
        /// <example>admin</example>
        public string Usuario { get; set; }

        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        /// <example>password123</example>
        public string Contrasena { get; set; }
    }
}
