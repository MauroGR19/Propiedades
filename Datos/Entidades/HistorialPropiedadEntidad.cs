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
        [Required]
        [StringLength(30)]
        public string MatriculaInmobiliaria { get; set; }  // FK a la propiedad
        
        // Campos de auditoría
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [StringLength(100)]
        public string CreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [StringLength(100)]
        public string? ModificadoPor { get; set; }
        
        public PropiedadEntidad Propiedad { get; set; }

    }
}
