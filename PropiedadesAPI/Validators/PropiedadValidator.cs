using FluentValidation;
using DTO.DTO;

namespace PropiedadesAPI.Validators
{
    /// <summary>
    /// Validador FluentValidation para el DTO de propiedades inmobiliarias
    /// </summary>
    /// <remarks>
    /// Define reglas de validación completas para propiedades:
    /// - Nombre: 2-150 caracteres
    /// - Dirección: 5-300 caracteres
    /// - Precio: mayor a 0, máximo 999,999,999.99
    /// - Código interno: formato específico con mayúsculas
    /// - Año: entre 1900 y año actual
    /// - Propietario: debe ser válido
    /// </remarks>
    public class PropiedadValidator : AbstractValidator<PropiedadDTO>
    {
        /// <summary>
        /// Constructor que define todas las reglas de validación para PropiedadDTO
        /// </summary>
        public PropiedadValidator()
        {
            // Validación del ID (para actualizaciones)
            RuleFor(x => x.MatriculaInmobiliaria)
                .NotEmpty()
                .WithMessage("La matrícula inmobiliaria es requerida")
                .Length(3, 30)
                .WithMessage("La matrícula inmobiliaria debe tener entre 3 y 30 caracteres");

            // Validaciones para el nombre de la propiedad
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre de la propiedad es requerido")
                .Length(2, 150)
                .WithMessage("El nombre debe tener entre 2 y 150 caracteres");

            // Validaciones para la dirección
            RuleFor(x => x.Direccion)
                .NotEmpty()
                .WithMessage("La dirección es requerida")
                .Length(5, 300)
                .WithMessage("La dirección debe tener entre 5 y 300 caracteres");

            // Validaciones para el precio
            RuleFor(x => x.Precio)
                .GreaterThan(0)
                .WithMessage("El precio debe ser mayor a 0")
                .LessThanOrEqualTo(999999999.99m)
                .WithMessage("El precio no puede exceder 999,999,999.99");

            // Validaciones para el código interno
            RuleFor(x => x.CodigoInterno)
                .NotEmpty()
                .WithMessage("El código interno es requerido")
                .Length(3, 20)
                .WithMessage("El código interno debe tener entre 3 y 20 caracteres")
                .Matches("^[A-Z0-9-]+$")
                .WithMessage("El código interno solo puede contener letras mayúsculas, números y guiones");

            // Validaciones para el año de construcción
            RuleFor(x => x.Anio)
                .GreaterThanOrEqualTo(1900)
                .WithMessage("El año no puede ser anterior a 1900")
                .LessThanOrEqualTo(DateTime.Now.Year)
                .WithMessage($"El año no puede ser posterior a {DateTime.Now.Year}");

            // Validación del propietario
            RuleFor(x => x. MatriculaInmobiliaria)
                .NotEmpty()
                .WithMessage("La matrícula inmobiliaria es requerida")
                .Length(3, 30)
                .WithMessage("La matrícula inmobiliaria debe tener entre 3 y 30 caracteres");
        }
    }
}
