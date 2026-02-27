using AgroNet.DTOs.UsuarioDto;

namespace AgroNet.Interfaces.Usuario
{
    public interface IPerfilService
    {
        Task<UsuarioReadDto> ActualizarPerfil(int usuarioId,UsuarioUpdateDto usuarioUpdateDto);
    }
}
