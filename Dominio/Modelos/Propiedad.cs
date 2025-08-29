using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class Propiedad
    {
        public int IdPropiedad { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public decimal Precio { get; set; }
        public string CodigoInterno { get; set; }
        public int Anio { get; set; }

        public Propiedad()
        {

            IdPropiedad = 0;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Precio = 0;
            CodigoInterno = string.Empty;
            Anio = 0;
        }

    }
}
