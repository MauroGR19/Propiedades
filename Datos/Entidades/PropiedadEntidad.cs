using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    public class PropiedadEntidad
    {
        [Key]
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

        public int IdPropietario { get; set; }
        
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
