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
            // Validación del ID (para actualizaciones)
            RuleFor(x => x.IdPropietario)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El ID del propietario debe ser mayor o igual a 0");

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
