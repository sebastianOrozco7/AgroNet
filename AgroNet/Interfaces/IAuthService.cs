using AgroNet.DTOs.UsuarioDto;

namespace AgroNet.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioReadDto> Registrar(UsuarioCreateDto usuarioCreateDto);
        Task<string?> Login(string Correo, string Password);
    }
}
