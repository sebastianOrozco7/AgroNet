using AutoMapper;
using AgroNet.Data;
using AgroNet.Models;
using AgroNet.DTOs.UsuarioDto;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using AgroNet.Interfaces.Usuario;
namespace AgroNet.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config; // esto es necesario para leer la clave jwt

        public AuthService(IMapper mapper, AppDbContext appDbContext, IConfiguration config)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _config = config;
        }

        public async Task<UsuarioReadDto> Registrar(UsuarioCreateDto usuarioCreateDto)
        {
            //Mapeo del UsuarioDto --> Usuario
            var UsuarioNuevo = _mapper.Map<Usuario>(usuarioCreateDto);

            // Encriptar la contraseña usando BCrypt
            UsuarioNuevo.Password = BCrypt.Net.BCrypt.HashPassword(usuarioCreateDto.Password);

            //guardar en la base de datos 
            _appDbContext.Usuarios.Add(UsuarioNuevo);
            await _appDbContext.SaveChangesAsync();

            //Mapeo de la entidad guardada al DTO de lectura para devolverlo (sin password)
            return _mapper.Map<UsuarioReadDto>(UsuarioNuevo);
        }
        public async Task<string?> Login(string Correo, string Password)
        {
            //busca el usuario por el correo
            var usuario = await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Correo == Correo);

            // 2. Si no existe, o la contraseña NO coincide (BCrypt.Verify desencripta y compara)
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(Password, usuario.Password))
            {
                return null; // Credenciales inválidas
            }

            return GenerarToken(usuario);
        }

        // --- MÉTODO PRIVADO PARA CREAR EL TOKEN ---
        private string GenerarToken(Usuario usuario)
        {
            // Leemos la configuración del appsettings.json
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Información que irá dentro del token (No pongas contraseñas aquí)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.IdRol.ToString())
            };

            // Armamos el token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1), // El token expira en 1 día
                signingCredentials: creds
            );

            // Lo devolvemos como texto (string)
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
