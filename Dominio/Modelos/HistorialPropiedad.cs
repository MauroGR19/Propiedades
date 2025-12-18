namespace Dominio.Modelos
{
    /// <summary>
    /// Entidad de dominio que representa el historial de transacciones de una propiedad
    /// </summary>
    public class HistorialPropiedad
    {
        /// <summary>
        /// Identificador único del registro de historial
        /// </summary>
        public int IdHistorialPropiedad { get; set; }

        /// <summary>
        /// Fecha en que se realizó la transacción
        /// </summary>
        public DateTime FechaVenta { get; set; }

        /// <summary>
        /// Nombre del comprador, vendedor o descripción de la transacción
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Valor monetario de la transacción en pesos colombianos
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Impuestos aplicados a la transacción
        /// </summary>
        public decimal Impuesto { get; set; }

        /// <summary>
        /// Identificador de la propiedad asociada a este historial
        /// </summary>
        public int IdPropiedad { get; set; }

        /// <summary>
        /// Constructor por defecto que inicializa el historial con valores predeterminados
        /// </summary>
        public HistorialPropiedad()
        {
            IdHistorialPropiedad = 0;
            FechaVenta = DateTime.Now;
            Nombre = string.Empty;
            Valor = 0;
            Impuesto = 0;
            IdPropiedad = 0;
        }
    }
}
