using AgroNet.DTOs.FincasDto;
using AgroNet.DTOs.UsuarioDto;
using AgroNet.Interfaces.Finca;
using AgroNet.Interfaces.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Security.Claims;
namespace AgroNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FincaController : ControllerBase
    {
        private readonly IFincaService _fincaService;

        public FincaController(IFincaService fincaService)
        {
            _fincaService = fincaService;
        }

        [HttpPost]
        public async Task<ActionResult<FincaReadDto>> RegistrarFinca(FincaCreateDto fincaCreateDto)
        {
            //se debe extraer el ID del dueño desde el token 
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized("Token invalido");

            //le asignamos el claim a la variable usuarioId
            var usuarioId = int.Parse(claim.Value);

            //utilizamos el metodo del servicio para crear la finca
            var NuevaFinca = await _fincaService.CrearFinca(usuarioId, fincaCreateDto);

            //retornamos la finca, mostrara el readDto ya que el servicio envio esa respuesta
            return Ok(NuevaFinca);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FincaReadDto>>> VerTodasLasFincas()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized("Token invalido");

            var usuarioId = int.Parse(claim.Value);

            //utilizamos el metodo del servicio
            var fincas = await _fincaService.VerFincasDelUsuario(usuarioId);

            //devoldemos la lista de fincas del usuario
            return Ok(fincas);

        }



    }
}
