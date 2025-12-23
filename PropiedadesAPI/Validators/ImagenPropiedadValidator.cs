using FluentValidation;
using DTO.DTO;
using System.IO;

namespace PropiedadesAPI.Validators
{
    /// <summary>
    /// Validador FluentValidation para el DTO de imágenes de propiedades
    /// </summary>
    /// <remarks>
    /// Define reglas de validación para imágenes:
    /// - Propiedad: debe ser válida
    /// - Archivo: extensiones de imagen válidas (.jpg, .jpeg, .png, .gif, .bmp)
    /// - Estado habilitado: requerido
    /// </remarks>
    public class ImagenPropiedadValidator : AbstractValidator<ImagenPropiedadDTO>
    {
        /// <summary>
        /// Constructor que define todas las reglas de validación para ImagenPropiedadDTO
        /// </summary>
        public ImagenPropiedadValidator()
        {
            // Validación del ID (para actualizaciones)
            RuleFor(x => x.IdImagenPropiedad)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El ID de la imagen debe ser mayor o igual a 0");

            // Validación de la propiedad asociada
            RuleFor(x => x.MatriculaInmobiliaria)
                .NotEmpty()
                .WithMessage("La matrícula inmobiliaria es requerida")
                .Length(3, 30)
                .WithMessage("La matrícula inmobiliaria debe tener entre 3 y 30 caracteres");

            // Validaciones para el archivo de imagen
            RuleFor(x => x.Archivo)
                .NotEmpty()
                .WithMessage("El archivo de imagen es requerido")
                .Must(BeValidImagePath)
                .WithMessage("El archivo debe tener una extensión válida (.jpg, .jpeg, .png, .gif, .bmp)");

            // Validación del estado habilitado
            RuleFor(x => x.Habilitado)
                .NotNull()
                .WithMessage("El estado habilitado es requerido");
        }

        /// <summary>
        /// Valida que el archivo tenga una extensión de imagen válida
        /// </summary>
        /// <param name="archivo">Nombre o ruta del archivo de imagen</param>
        /// <returns>true si la extensión es válida, false en caso contrario</returns>
        /// <remarks>
        /// Extensiones válidas: .jpg, .jpeg, .png, .gif, .bmp
        /// </remarks>
        private bool BeValidImagePath(string archivo)
        {
            if (string.IsNullOrEmpty(archivo))
                return false;

            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(archivo)?.ToLower();
            
            return validExtensions.Contains(extension);
        }
    }
}
