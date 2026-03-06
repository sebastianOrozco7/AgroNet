using AutoMapper;
using AgroNet.Models;
using AgroNet.DTOs;
using AgroNet.DTOs.UsuarioDto;
using AgroNet.DTOs.FincasDto;
using AgroNet.DTOs.CosechaDto;

namespace AgroNet.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //USUARIO//

            //Mapeo de UsuarioCreateDto --> Usuario
            CreateMap<UsuarioCreateDto, Usuario>();
            //Mapeo de Usuario --> UsuarioReadDto
            CreateMap<Usuario, UsuarioReadDto>();
            //Mapeo de UsuarioUpdate --> Usuario
            CreateMap<UsuarioUpdateDto, Usuario>();


            //FINCAS//
            
            //Mapeo de FincaCreateDto --> Finca
            CreateMap<FincaCreateDto, Finca>();
            //Mapeo de Finca --> FincaReadDto
            CreateMap<Finca, FincaReadDto>();
            //Mapeo de FincaUpdateDto --> Finca
            CreateMap<FincaUpdateDto, Finca>();


            //COSECHAS//
            
            //Mapeo de CosechaCreateDto --> Cosecha
            CreateMap<CosechaCreateDto, Cosecha>();
            //Mapeo de Cosecha --> CosechaReadDto
            CreateMap<Cosecha, CosechaReadDto>()
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.ToString())); // Uso .ToString() para que el número (1,2,3,4) se convierta en la palabra ("Disponible")
            //Mapeo de CosechaUpdate --> Cosecha
            CreateMap<CosechaUpdateDto, Cosecha>();
        }
    }
}
