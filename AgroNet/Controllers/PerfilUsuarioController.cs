using AgroNet.DTOs.UsuarioDto;
using AgroNet.Interfaces.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgroNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilUsuarioController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfilUsuarioController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }


        // PUT api/<PerfilUsuarioController>/5
        [Authorize]
        [HttpPut("Actualizar")]
        public async Task<ActionResult<UsuarioReadDto>> Actualizar(UsuarioUpdateDto update)
        {
            //validamos si es nulo
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized("No se encontró el ID en el token");

            // Extraemos el ID del Claim del Token
            var usuarioId = int.Parse(claim.Value);

            var usuarioActualizado = await _perfilService.ActualizarPerfil(usuarioId, update);

            if (usuarioActualizado == null)
                return NotFound("Usuario no Encontrado");

            return Ok(usuarioActualizado);
        }

       
    }
}
