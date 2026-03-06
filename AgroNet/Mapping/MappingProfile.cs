using AutoMapper;
using AgroNet.Models;
using AgroNet.DTOs;
using AgroNet.DTOs.UsuarioDto;
using AgroNet.DTOs.FincasDto;

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
        }
    }
}
