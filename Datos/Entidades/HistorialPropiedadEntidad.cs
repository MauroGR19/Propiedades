using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    public class HistorialPropiedadEntidad
    {
        [Key]
        public int IdHistorialPropiedad { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
        [Required]
        [StringLength(20)]
        public string Nombre { get; set; }        // Comprador, vendedor o referencia de la venta
        [Required]
        public decimal Valor { get; set; }        // Valor de la venta
        [Required]
        public decimal Impuesto { get; set; }     // Impuesto aplicado
        public int IdPropiedad { get; set; }
        public PropiedadEntidad Propiedad { get; set; }

    }
}
