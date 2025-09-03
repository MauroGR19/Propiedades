using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class AutenticacionDTO
    {
        [Required]
        public string usuario { get; set; }
        [Required]
        public string contrasena { get; set; }
    }
}
