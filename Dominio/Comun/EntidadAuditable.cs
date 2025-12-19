namespace Dominio.Comun
{
    /// <summary>
    /// Clase base que proporciona campos de auditoría para todas las entidades
    /// </summary>
    public abstract class EntidadAuditable
    {
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string CreadoPor { get; set; }

        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime? FechaModificacion { get; set; }

        /// <summary>
        /// Usuario que realizó la última modificación
        /// </summary>
        public string? ModificadoPor { get; set; }

        /// <summary>
        /// Constructor que inicializa los campos de auditoría
        /// </summary>
        protected EntidadAuditable()
        {
            FechaCreacion = DateTime.UtcNow;
            CreadoPor = "Sistema";
            FechaModificacion = null;
            ModificadoPor = null;
        }
    }
}