namespace DTO.DTO
{
    /// <summary>
    /// DTO para propiedades inmobiliarias
    /// </summary>
    public class PropiedadDTO
    {
        /// <summary>
        /// Matrícula inmobiliaria oficial de la propiedad
        /// </summary>
        /// <example>050-0001234</example>
        public string MatriculaInmobiliaria { get; set; }

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
        /// Número de documento del propietario de la propiedad
        /// </summary>
        /// <example>12345678</example>
        public string NumeroDocumentoPropietario { get; set; }
    }
}
