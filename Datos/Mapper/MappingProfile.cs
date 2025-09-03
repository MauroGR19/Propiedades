using AutoMapper;
using Datos.Entidades;
using Dominio.Modelos;

namespace Datos.Mapper
{
    public class MappingProfile : Profile
    {
        #region Constructor
        public MappingProfile()
        {
            CreateMap<Autenticacion, AutenticacionEntidad>().ReverseMap();
            CreateMap<HistorialPropiedad, HistorialPropiedadEntidad>().ReverseMap();
            CreateMap<ImagenPropiedad, ImagenPropiedadEntidad>().ReverseMap();
            CreateMap<Propiedad, PropiedadEntidad>().ReverseMap();
            CreateMap<Propietario, PropietarioEntidad>().ReverseMap();
        }
        #endregion
    }
}
