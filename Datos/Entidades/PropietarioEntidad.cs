using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    public class PropietarioEntidad
    {
        [Key]
        [StringLength(20)]
        public string NumeroDocumento { get; set; }  // Cédula, Pasaporte, NIT
        
        [Required]
        [StringLength(10)]
        public string TipoDocumento { get; set; }    // CC, CE, PA, NIT
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(200)]
        public string Direccion { get; set; }
        [Required]
        public string Foto { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
        
        // Campos de auditoría
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [StringLength(100)]
        public string CreadoPor { get; set; }
        public DateTime? FechaModificacion { get; set; }
        [StringLength(100)]
        public string? ModificadoPor { get; set; }
        
        public AutenticacionEntidad Autenticacion { get; set; }
        public IList<PropiedadEntidad> Propiedad { get; set; }
    }
}
