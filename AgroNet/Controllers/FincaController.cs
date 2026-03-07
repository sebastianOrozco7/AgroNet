using AgroNet.DTOs.FincasDto;
using AgroNet.Interfaces.Finca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var Fincas = await _fincaService.VerFincasDelUsuario(usuarioId);

            //devoldemos la lista de fincas del usuario
            return Ok(Fincas);

        }

        [HttpGet("{FincaId}")]
        public async Task<ActionResult<FincaReadDto>> VerFincaPorId(int FincaId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized("Token invalido");

            var UsuarioId = int.Parse(claim.Value);

            var Finca = await _fincaService.VerFincaPorId(UsuarioId, FincaId);

            if(Finca == null) return NotFound($"No se encontro la finca con el ID {FincaId}");

            return Ok(Finca);
        }

        [HttpPut("{fincaId}")]
        public async Task<ActionResult<FincaReadDto>> ActualizarFinca(int fincaId, FincaUpdateDto fincaUpdateDto)
        {

            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized("Token invalido");

            var UsuarioId = int.Parse(claim.Value);

            var Finca = await _fincaService.ActualizarFinca(fincaId, UsuarioId,fincaUpdateDto);

            if (Finca == null) return NotFound($"No se encontro la finca con el ID {fincaId}");

            return Ok(Finca);
        }

        [HttpDelete("{fincaId}")]
        public async Task<ActionResult<bool>> EliminarFinca(int fincaId)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return Unauthorized("Token invalido");

            var UsuarioId = int.Parse(claim.Value);

            var fincaEliminar = await _fincaService.EliminarFinca(fincaId, UsuarioId);

            if (!fincaEliminar) return NotFound($"No se encontro la finca con el ID {fincaId}");

            //se elimino correctamente y ya no hay nada que devolver por eso el NoContent
            return NoContent();
        }



    }
}
