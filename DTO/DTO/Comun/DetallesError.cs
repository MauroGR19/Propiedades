using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTO.Comun
{
    public class DetallesError
    {
        public int CodigoEstado { get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; }
        public string Detalles { get; set; }
        public DateTime FechaHora { get; set; }
        public string Ruta { get; set; }

        public DetallesError()
        {
            FechaHora = DateTime.UtcNow;
        }
    }
}
