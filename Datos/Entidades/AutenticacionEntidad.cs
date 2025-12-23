using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    public class AutenticacionEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Usuario { get; set; }

        [Required, StringLength(100)]
        public string Contrasena { get; set; }

        // FK opcional
        [StringLength(20)]
        public string? NumeroDocumentoPropietario { get; set; }
        public PropietarioEntidad PropietarioEntidad { get; set; }
    }
}
