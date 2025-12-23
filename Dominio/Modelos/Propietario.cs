using Dominio.Comun;

namespace Dominio.Modelos
{
    /// <summary>
    /// Entidad de dominio que representa un propietario de inmuebles
    /// </summary>
    public class Propietario : EntidadAuditable
    {
        /// <summary>
        /// Número de documento de identidad del propietario (Cédula, Pasaporte, NIT)
        /// </summary>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Tipo de documento (CC, CE, PA, NIT)
        /// </summary>
        public string TipoDocumento { get; set; }

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
            NumeroDocumento = string.Empty;
            TipoDocumento = string.Empty;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Foto = string.Empty;
            FechaNacimiento = DateTime.Now;
        }
    }
}
