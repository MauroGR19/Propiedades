using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    public class PropietarioEntidad
    {
        [Key]
        public int IdPropietario { get; set; }
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
        public AutenticacionEntidad Autenticacion { get; set; }
        public IList<PropiedadEntidad> Propiedad { get; set; }
    }
}
