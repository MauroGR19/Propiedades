namespace Dominio.Modelos
{
    public class HistorialPropiedad
    {
        public int IdHistorialPropiedad { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Nombre { get; set; }        // Comprador, vendedor o referencia de la venta
        public decimal Valor { get; set; }        // Valor de la venta
        public decimal Impuesto { get; set; }     // Impuesto aplicado
        public int IdPropiedad { get; set; }
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
