using System.ComponentModel.DataAnnotations;

namespace DTO.DTO
{
    public class ImagenPropiedadDTO
    {
        [Required]
        public int IdImagenPropiedad { get; set; }
        [Required]
        public int IdPropiedad { get; set; }
        [Required]
        public string Archivo { get; set; }
        [Required]
        public bool Habilitado { get; set; }
    }
}
