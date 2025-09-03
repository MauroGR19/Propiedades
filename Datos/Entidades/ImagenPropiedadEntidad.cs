using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    public class ImagenPropiedadEntidad
    {
        [Key]
        public int IdImagenPropiedad { get; set; }

        // Relación con Propiedad
        [Required]
        public int IdPropiedad { get; set; }
        [Required]
        public string Archivo { get; set; }
        [Required]
        public bool Habilitado { get; set; }

        public PropiedadEntidad propiedad { get; set; }
    }
}
