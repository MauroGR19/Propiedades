using AutoMapper;
using Datos.Entidades;
using Dominio.Modelos;

namespace Datos.Mapper
{
    /// <summary>
    /// Perfil de configuración de AutoMapper para el mapeo entre entidades de dominio y entidades de Entity Framework
    /// </summary>
    /// <remarks>
    /// Define las reglas de mapeo bidireccional entre:
    /// - Entidades de dominio (modelos de negocio)
    /// - Entidades de Entity Framework (modelos de base de datos)
    /// Esto permite mantener separadas las capas de dominio y datos
    /// </remarks>
    public class MappingProfile : Profile
    {
        #region Constructor
        /// <summary>
        /// Constructor que configura todos los mapeos entre entidades de dominio y entidades de EF
        /// </summary>
        public MappingProfile()
        {
            // Mapeo bidireccional entre modelos de dominio y entidades de base de datos
            CreateMap<Autenticacion, AutenticacionEntidad>().ReverseMap();
            CreateMap<HistorialPropiedad, HistorialPropiedadEntidad>().ReverseMap();
            CreateMap<ImagenPropiedad, ImagenPropiedadEntidad>().ReverseMap();
            CreateMap<Propiedad, PropiedadEntidad>().ReverseMap();
            CreateMap<Propietario, PropietarioEntidad>().ReverseMap();
        }
        #endregion
    }
}
