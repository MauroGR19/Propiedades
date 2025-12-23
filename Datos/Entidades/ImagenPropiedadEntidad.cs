using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    public class ImagenPropiedadEntidad
    {
        [Key]
        public int IdImagenPropiedad { get; set; }

        // Relación con Propiedad
        [Required]
        [StringLength(30)]
        public string MatriculaInmobiliaria { get; set; }  // FK a la propiedad
        [Required]
        public string Archivo { get; set; }
        [Required]
        public bool Habilitado { get; set; }
        
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
