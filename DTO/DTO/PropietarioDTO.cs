using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class PropietarioDTO
    {
        [Required]
        public int IdPropietario { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Foto { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
    }
}
