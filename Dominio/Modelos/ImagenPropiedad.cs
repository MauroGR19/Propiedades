using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ImagenPropiedad
    {
        public int IdImagenPropiedad { get; set; }

        // Relación con Propiedad
        public int IdPropiedad { get; set; }
        public string Archivo { get; set; }
        public bool Habilitado { get; set; }

        public ImagenPropiedad()
        {

            IdPropiedad = 0;
            Archivo = string.Empty;
            Habilitado = false;
        }
    }
}
