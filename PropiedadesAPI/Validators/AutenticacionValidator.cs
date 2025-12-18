using FluentValidation;
using DTO.DTO;

namespace PropiedadesAPI.Validators
{
    /// <summary>
    /// Validador FluentValidation para el DTO de autenticación
    /// </summary>
    /// <remarks>
    /// Define las reglas de validación para los datos de autenticación:
    /// - Usuario: 3-50 caracteres, solo alfanuméricos y algunos símbolos
    /// - Contraseña: 6-100 caracteres mínimo
    /// 
    /// Se ejecuta automáticamente antes de que los datos lleguen a los controladores
    /// </remarks>
    public class AutenticacionValidator : AbstractValidator<AutenticacionDTO>
    {
        /// <summary>
        /// Constructor que define todas las reglas de validación para AutenticacionDTO
        /// </summary>
        public AutenticacionValidator()
        {
            // Validaciones para el campo Usuario
            RuleFor(x => x.Usuario)
                .NotEmpty()
                .WithMessage("El usuario es requerido")
                .Length(3, 50)
                .WithMessage("El usuario debe tener entre 3 y 50 caracteres")
                .Matches("^[a-zA-Z0-9._-]+$")
                .WithMessage("El usuario solo puede contener letras, números, puntos, guiones y guiones bajos");

            // Validaciones para el campo Contraseña
            RuleFor(x => x.Contrasena)
                .NotEmpty()
                .WithMessage("La contraseña es requerida")
                .MinimumLength(6)
                .WithMessage("La contraseña debe tener al menos 6 caracteres")
                .MaximumLength(100)
                .WithMessage("La contraseña no puede exceder 100 caracteres");
        }
    }
}
