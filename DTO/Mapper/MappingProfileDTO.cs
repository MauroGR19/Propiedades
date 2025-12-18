using AutoMapper;
using Dominio.Modelos;
using DTO.DTO;
using Dominio.Comun;
using DTO.DTO.Comun;

namespace DTO.Mapper
{
    /// <summary>
    /// Perfil de configuración de AutoMapper para el mapeo entre entidades de dominio y DTOs
    /// </summary>
    /// <remarks>
    /// Define las reglas de mapeo bidireccional entre:
    /// - Entidades de dominio (capa de negocio)
    /// - DTOs (objetos de transferencia de datos para la API)
    /// </remarks>
    public class MappingProfileDTO : Profile
    {
        #region Constructor
        /// <summary>
        /// Constructor que configura todos los mapeos entre entidades de dominio y DTOs
        /// </summary>
        public MappingProfileDTO()
        {
            // Mapeo bidireccional de entidades principales
            CreateMap<Autenticacion, AutenticacionDTO>().ReverseMap();
            CreateMap<HistorialPropiedad, HistorialPropiedadDTO>().ReverseMap();
            CreateMap<ImagenPropiedad, ImagenPropiedadDTO>().ReverseMap();
            CreateMap<Propiedad, PropiedadDTO>().ReverseMap();
            CreateMap<Propietario, PropietarioDTO>().ReverseMap();
            
            // Mapeo de objetos de paginación
            CreateMap<PaginacionParametros, PaginacionRequest>().ReverseMap();
            CreateMap(typeof(ResultadoPaginado<>), typeof(PaginacionResponse<>)).ReverseMap();
        }
        #endregion
    }
}
