using AgroNet.DTOs.UsuarioDto;

namespace AgroNet.Interfaces.Usuario
{
    public interface IPerfilService
    {
        //Actualizar
        Task<UsuarioReadDto> ActualizarPerfil(int usuarioId,UsuarioUpdateDto usuarioUpdateDto);
    }
}
