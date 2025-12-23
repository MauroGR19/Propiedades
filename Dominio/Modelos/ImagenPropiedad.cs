using Dominio.Comun;

namespace Dominio.Modelos
{
    /// <summary>
    /// Entidad de dominio que representa una imagen asociada a una propiedad
    /// </summary>
    public class ImagenPropiedad : EntidadAuditable
    {
        /// <summary>
        /// Identificador único de la imagen
        /// </summary>
        public int IdImagenPropiedad { get; set; }

        /// <summary>
        /// Matrícula inmobiliaria de la propiedad a la que pertenece esta imagen
        /// </summary>
        public string MatriculaInmobiliaria { get; set; }

        /// <summary>
        /// Nombre del archivo de imagen o URL donde se almacena
        /// </summary>
        public string Archivo { get; set; }

        /// <summary>
        /// Indica si la imagen está habilitada para ser mostrada públicamente
        /// </summary>
        public bool Habilitado { get; set; }

        /// <summary>
        /// Constructor por defecto que inicializa la imagen con valores predeterminados
        /// </summary>
        public ImagenPropiedad()
        {
            MatriculaInmobiliaria = string.Empty;
            Archivo = string.Empty;
            Habilitado = false;
        }
    }
}
