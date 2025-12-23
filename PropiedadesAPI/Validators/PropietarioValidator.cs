using FluentValidation;
using DTO.DTO;
using System.IO;

namespace PropiedadesAPI.Validators
{
    /// <summary>
    /// Validador FluentValidation para el DTO de propietarios
    /// </summary>
    /// <remarks>
    /// Define reglas de validación para propietarios:
    /// - Nombre: 2-100 caracteres, solo letras y espacios (incluye acentos)
    /// - Dirección: 5-200 caracteres
    /// - Foto: extensiones de imagen válidas
    /// - Fecha nacimiento: no futura, no mayor a 120 años
    /// </remarks>
    public class PropietarioValidator : AbstractValidator<PropietarioDTO>
    {
        /// <summary>
        /// Constructor que define todas las reglas de validación para PropietarioDTO
        /// </summary>
        public PropietarioValidator()
        {
            // Validación del número de documento
            RuleFor(x => x.NumeroDocumento)
                .NotEmpty()
                .WithMessage("El número de documento es requerido")
                .Length(5, 20)
                .WithMessage("El número de documento debe tener entre 5 y 20 caracteres")
                .Matches("^[0-9A-Z-]+$")
                .WithMessage("El número de documento solo puede contener números, letras mayúsculas y guiones");

            // Validación del tipo de documento
            RuleFor(x => x.TipoDocumento)
                .NotEmpty()
                .WithMessage("El tipo de documento es requerido")
                .Must(BeValidDocumentType)
                .WithMessage("El tipo de documento debe ser CC, CE, PA o NIT");

            // Validaciones para el nombre (incluye caracteres en español)
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre es requerido")
                .Length(2, 100)
                .WithMessage("El nombre debe tener entre 2 y 100 caracteres")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$")
                .WithMessage("El nombre solo puede contener letras y espacios");

            // Validaciones para la dirección
            RuleFor(x => x.Direccion)
                .NotEmpty()
                .WithMessage("La dirección es requerida")
                .Length(5, 200)
                .WithMessage("La dirección debe tener entre 5 y 200 caracteres");

            // Validaciones para la foto (debe ser imagen válida)
            RuleFor(x => x.Foto)
                .NotEmpty()
                .WithMessage("La foto es requerida")
                .Must(BeValidImagePath)
                .WithMessage("La foto debe tener una extensión válida (.jpg, .jpeg, .png, .gif, .bmp)");

            // Validaciones para la fecha de nacimiento
            RuleFor(x => x.FechaNacimiento)
                .NotEmpty()
                .WithMessage("La fecha de nacimiento es requerida")
                .LessThan(DateTime.Now)
                .WithMessage("La fecha de nacimiento no puede ser futura")
                .GreaterThan(DateTime.Now.AddYears(-120))
                .WithMessage("La fecha de nacimiento no puede ser anterior a 120 años");
        }

        /// <summary>
        /// Valida que el tipo de documento sea válido
        /// </summary>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <returns>true si el tipo es válido, false en caso contrario</returns>
        private bool BeValidDocumentType(string tipoDocumento)
        {
            if (string.IsNullOrEmpty(tipoDocumento))
                return false;

            var validTypes = new[] { "CC", "CE", "PA", "NIT" };
            return validTypes.Contains(tipoDocumento.ToUpper());
        }

        /// <summary>
        /// Valida que la ruta de la foto tenga una extensión de imagen válida
        /// </summary>
        /// <param name="foto">Ruta o nombre del archivo de foto</param>
        /// <returns>true si la extensión es válida, false en caso contrario</returns>
        private bool BeValidImagePath(string foto)
        {
            if (string.IsNullOrEmpty(foto))
                return false;

            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(foto)?.ToLower();
            
            return validExtensions.Contains(extension);
        }
    }
}
