using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class HistorialPropiedadDTO
    {
        [Required]
        public int IdHistorialPropiedad { get; set; }
        [Required]
        public DateTime FechaVenta { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public decimal Impuesto { get; set; }
        [Required]
        public int idPropiedad { get; set; }

    }
}
