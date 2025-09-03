using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class PropiedadDTO
    {
        [Required]
        public int IdPropiedad { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public string CodigoInterno { get; set; }
        [Required]
        public int Anio { get; set; }
        [Required]
        public int IdPropietario { get; set; }
    }
}
