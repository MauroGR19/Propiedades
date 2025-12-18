namespace DTO.DTO
{
    /// <summary>
    /// DTO para propiedades inmobiliarias
    /// </summary>
    public class PropiedadDTO
    {
        /// <summary>
        /// Identificador único de la propiedad
        /// </summary>
        /// <example>1</example>
        public int IdPropiedad { get; set; }

        /// <summary>
        /// Nombre descriptivo de la propiedad
        /// </summary>
        /// <example>Casa en el centro</example>
        public string Nombre { get; set; }

        /// <summary>
        /// Dirección completa de la propiedad
        /// </summary>
        /// <example>Calle 123 #45-67, Bogotá</example>
        public string Direccion { get; set; }

        /// <summary>
        /// Precio de la propiedad en pesos colombianos
        /// </summary>
        /// <example>250000000</example>
        public decimal Precio { get; set; }

        /// <summary>
        /// Código interno único de la propiedad
        /// </summary>
        /// <example>PROP-001</example>
        public string CodigoInterno { get; set; }

        /// <summary>
        /// Año de construcción de la propiedad
        /// </summary>
        /// <example>2020</example>
        public int Anio { get; set; }

        /// <summary>
        /// Identificador del propietario de la propiedad
        /// </summary>
        /// <example>1</example>
        public int IdPropietario { get; set; }
    }
}
