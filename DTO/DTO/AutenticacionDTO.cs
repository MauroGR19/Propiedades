using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class AutenticacionDTO
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Contrasena { get; set; }
    }
}
