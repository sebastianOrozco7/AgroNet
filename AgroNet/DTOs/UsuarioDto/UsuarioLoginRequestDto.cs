namespace AgroNet.DTOs.UsuarioDto
{
    public class UsuarioLoginRequestDto
    {
        public required string Correo { get; set; }
        public required string Password { get; set; }
    }
}
