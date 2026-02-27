using AgroNet.Data;
using AgroNet.DTOs.UsuarioDto;
using AgroNet.Interfaces.Usuario;
using AgroNet.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgroNet.Services
{
    public class PerfilService : IPerfilService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public PerfilService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<UsuarioReadDto> ActualizarPerfil(int usuarioId, UsuarioUpdateDto usuarioUpdateDto)
        {
            var UsuarioExistente = await _appDbContext.Usuarios.FindAsync(usuarioId);

            if (UsuarioExistente == null)
                return null;

            //mapeo de DTO --> usuario
            _mapper.Map(usuarioUpdateDto, UsuarioExistente);

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<UsuarioReadDto>(UsuarioExistente);
        }
    }
}

