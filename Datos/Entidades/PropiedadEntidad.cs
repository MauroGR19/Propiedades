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

        public int idPropietario { get; set; }
        public PropietarioEntidad Propietario { get; set; }
        public IList<ImagenPropiedadEntidad> imagen { get; set; }
        public IList<HistorialPropiedadEntidad> historial { get; set; }
    }
}
