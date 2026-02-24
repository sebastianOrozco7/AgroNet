using AutoMapper;
using AgroNet.Models;
using AgroNet.DTOs;
using AgroNet.DTOs.UsuarioDto;

namespace AgroNet.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Mapeo de UsuarioCreateDto --> Usuario
            CreateMap<UsuarioCreateDto, Usuario>();

            //Mapeo de Usuario --> UsuarioReadDto
            CreateMap<Usuario, UsuarioReadDto>();
        }
    }
}
