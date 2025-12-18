using FluentValidation;
using DTO.DTO;

namespace PropiedadesAPI.Validators
{
    /// <summary>
    /// Validador FluentValidation para el DTO de historial de propiedades
    /// </summary>
    /// <remarks>
    /// Define reglas de validación para transacciones de propiedades:
    /// - Fecha venta: no futura, no mayor a 50 años
    /// - Nombre: 2-150 caracteres, solo letras (incluye acentos)
    /// - Valor: mayor a 0, máximo 999,999,999.99
    /// - Impuesto: no negativo, máximo 99,999,999.99
    /// - Propiedad: debe ser válida
    /// </remarks>
    public class HistorialPropiedadValidator : AbstractValidator<HistorialPropiedadDTO>
    {
        /// <summary>
        /// Constructor que define todas las reglas de validación para HistorialPropiedadDTO
        /// </summary>
        public HistorialPropiedadValidator()
        {
            // Validación del ID (para actualizaciones)
            RuleFor(x => x.IdHistorialPropiedad)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El ID del historial debe ser mayor o igual a 0");

            // Validaciones para la fecha de venta
            RuleFor(x => x.FechaVenta)
                .NotEmpty()
                .WithMessage("La fecha de venta es requerida")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("La fecha de venta no puede ser futura")
                .GreaterThan(DateTime.Now.AddYears(-50))
                .WithMessage("La fecha de venta no puede ser anterior a 50 años");

            // Validaciones para el nombre del comprador/vendedor
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre del comprador es requerido")
                .Length(2, 150)
                .WithMessage("El nombre debe tener entre 2 y 150 caracteres")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$")
                .WithMessage("El nombre solo puede contener letras y espacios");

            // Validaciones para el valor de la transacción
            RuleFor(x => x.Valor)
                .GreaterThan(0)
                .WithMessage("El valor de venta debe ser mayor a 0")
                .LessThanOrEqualTo(999999999.99m)
                .WithMessage("El valor no puede exceder 999,999,999.99");

            // Validaciones para el impuesto
            RuleFor(x => x.Impuesto)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El impuesto no puede ser negativo")
                .LessThanOrEqualTo(99999999.99m)
                .WithMessage("El impuesto no puede exceder 99,999,999.99");

            // Validación de la propiedad asociada
            RuleFor(x => x.IdPropiedad)
                .GreaterThan(0)
                .WithMessage("Debe seleccionar una propiedad válida");
        }
    }
}
