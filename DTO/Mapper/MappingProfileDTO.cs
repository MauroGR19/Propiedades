using AutoMapper;
using Dominio.Modelos;
using DTO.DTO;

namespace DTO.Mapper
{
    public class MappingProfileDTO : Profile
    {
        #region Constructor
        public MappingProfileDTO()
        {
            CreateMap<Autenticacion, AutenticacionDTO>().ReverseMap();
            CreateMap<HistorialPropiedad, HistorialPropiedadDTO>().ReverseMap();
            CreateMap<ImagenPropiedad, ImagenPropiedadDTO>().ReverseMap();
            CreateMap<Propiedad, PropiedadDTO>().ReverseMap();
            CreateMap<Propietario, PropietarioDTO>().ReverseMap();
        }
        #endregion
    }
}
