namespace Dominio.Modelos
{
    /// <summary>
    /// Entidad de dominio que representa las credenciales de autenticación de un usuario
    /// </summary>
    public class Autenticacion
    {
        /// <summary>
        /// Nombre de usuario único en el sistema
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Contraseña del usuario (almacenada como hash)
        /// </summary>
        public string Contrasena { get; set; }

        /// <summary>
        /// Constructor por defecto que inicializa las credenciales con valores vacíos
        /// </summary>
        public Autenticacion()
        {
            Usuario = string.Empty;
            Contrasena = string.Empty;
        }
    }
}
