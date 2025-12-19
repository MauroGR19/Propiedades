using Dominio.Comun;

namespace Dominio.Modelos
{
    /// <summary>
    /// Entidad de dominio que representa una propiedad inmobiliaria
    /// </summary>
    public class Propiedad : EntidadAuditable
    {
        /// <summary>
        /// Identificador único de la propiedad
        /// </summary>
        public int IdPropiedad { get; set; }

        /// <summary>
        /// Nombre descriptivo de la propiedad
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Dirección completa donde se ubica la propiedad
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Precio de la propiedad en pesos colombianos
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// Código interno único para identificación administrativa
        /// </summary>
        public string CodigoInterno { get; set; }

        /// <summary>
        /// Año de construcción de la propiedad
        /// </summary>
        public int Anio { get; set; }

        /// <summary>
        /// Identificador del propietario de la propiedad
        /// </summary>
        public int IdPropietario { get; set; }

        /// <summary>
        /// Constructor por defecto que inicializa la propiedad con valores predeterminados
        /// </summary>
        public Propiedad()
        {
            IdPropiedad = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Precio = 0;
            CodigoInterno = string.Empty;
            Anio = 0;
            IdPropietario = 0;
        }
    }
}
