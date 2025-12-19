using Dominio.Comun;

namespace Dominio.Modelos
{
    /// <summary>
    /// Entidad de dominio que representa un propietario de inmuebles
    /// </summary>
    public class Propietario : EntidadAuditable
    {
        /// <summary>
        /// Identificador único del propietario
        /// </summary>
        public int IdPropietario { get; set; }

        /// <summary>
        /// Nombre completo del propietario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Dirección de residencia del propietario
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// URL o ruta del archivo de foto del propietario
        /// </summary>
        public string Foto { get; set; }

        /// <summary>
        /// Fecha de nacimiento del propietario
        /// </summary>
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Constructor por defecto que inicializa el propietario con valores predeterminados
        /// </summary>
        public Propietario()
        {
            IdPropietario = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Foto = string.Empty;
            FechaNacimiento = DateTime.Now;
        }
    }
}
