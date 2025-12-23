namespace DTO.DTO
{
    /// <summary>
    /// DTO para el historial de transacciones de propiedades
    /// </summary>
    public class HistorialPropiedadDTO
    {
        /// <summary>
        /// Identificador único del registro de historial
        /// </summary>
        /// <example>1</example>
        public int IdHistorialPropiedad { get; set; }

        /// <summary>
        /// Fecha en que se realizó la transacción
        /// </summary>
        /// <example>2024-01-15</example>
        public DateTime FechaVenta { get; set; }

        /// <summary>
        /// Nombre o descripción de la transacción
        /// </summary>
        /// <example>Venta a Juan Pérez</example>
        public string Nombre { get; set; }

        /// <summary>
        /// Valor de la transacción en pesos colombianos
        /// </summary>
        /// <example>250000000</example>
        public decimal Valor { get; set; }

        /// <summary>
        /// Impuestos aplicados a la transacción
        /// </summary>
        /// <example>5000000</example>
        public decimal Impuesto { get; set; }

        /// <summary>
        /// Matrícula inmobiliaria de la propiedad asociada
        /// </summary>
        /// <example>050-0001234</example>
        public string MatriculaInmobiliaria { get; set; }
    }
}
