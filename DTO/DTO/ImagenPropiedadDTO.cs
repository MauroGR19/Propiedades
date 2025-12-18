namespace DTO.DTO
{
    /// <summary>
    /// DTO para imágenes de propiedades
    /// </summary>
    public class ImagenPropiedadDTO
    {
        /// <summary>
        /// Identificador único de la imagen
        /// </summary>
        /// <example>1</example>
        public int IdImagenPropiedad { get; set; }

        /// <summary>
        /// Identificador de la propiedad asociada
        /// </summary>
        /// <example>1</example>
        public int IdPropiedad { get; set; }

        /// <summary>
        /// Nombre del archivo de imagen o URL
        /// </summary>
        /// <example>casa_fachada.jpg</example>
        public string Archivo { get; set; }

        /// <summary>
        /// Indica si la imagen está habilitada para mostrar
        /// </summary>
        /// <example>true</example>
        public bool Habilitado { get; set; }
    }
}
