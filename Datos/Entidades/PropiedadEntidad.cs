using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    public class PropiedadEntidad
    {
        [Key]
        [StringLength(30)]
        public string MatriculaInmobiliaria { get; set; }  // Matrícula oficial del inmueble
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(300)]
        public string Direccion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        [StringLength(20)]
        public string CodigoInterno { get; set; }  // Código interno de la empresa
        [Required]
        public int Anio { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroDocumentoPropietario { get; set; }  // FK al propietario
        
        // Campos de auditoría
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [StringLength(100)]
        public string CreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [StringLength(100)]
        public string? ModificadoPor { get; set; }
        
        public PropietarioEntidad Propietario { get; set; }
        public IList<ImagenPropiedadEntidad> Imagen { get; set; }
        public IList<HistorialPropiedadEntidad> Historial { get; set; }
    }
}
