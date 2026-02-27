using Microsoft.AspNetCore.Mvc;
using AgroNet.Interfaces;
using AgroNet.DTOs.UsuarioDto;
using Microsoft.AspNetCore.Identity.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgroNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        

        // POST api/<AuthController>
        [HttpPost("Registrar")]
        public async Task<ActionResult<UsuarioReadDto>> Registrar(UsuarioCreateDto usuarioCreateDto)
        {
            try
            {
                var resultado = await _authService.Registrar(usuarioCreateDto);
                return Ok(resultado);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al registrar Usuario: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UsuarioLoginRequestDto LoginDto)
        {
            var token = await _authService.Login(LoginDto.Correo, LoginDto.Password);

            if (token == null)
                return Unauthorized("Correo o Contraseña incorrecta");

            return Ok(new { token });
        }
       

        
    }
}
