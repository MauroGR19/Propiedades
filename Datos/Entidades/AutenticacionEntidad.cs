using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datos.Entidades
{
    public class AutenticacionEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string usuario { get; set; }

        [Required, StringLength(20)]
        public string contrasena { get; set; }

        // FK opcional
        public int? PropietarioId { get; set; }
        public PropietarioEntidad PropietarioEntidad { get; set; }
    }
}
