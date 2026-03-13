using AgroNet.DTOs.CosechaDto;
using AgroNet.Interfaces.Cosecha;
using AgroNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgroNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CosechaController : ControllerBase
    {

        private readonly ICosechaService _cosechaService;

        public CosechaController(ICosechaService cosechaService)
        {
            _cosechaService = cosechaService;
        }

        //metodo para refactorizar el codigo y obtener el token
        private int ObtenerUsuarioIdDelToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(claim.Value);
        }

        [HttpPost]
        public async Task<ActionResult<CosechaReadDto>> RegistrarCosecha (CosechaCreateDto cosechaCreateDto)
        {
            //le asignamos el claim a la variable usuarioId
            int usuarioId = ObtenerUsuarioIdDelToken();

            //usamos el metodo del servicio para crear la cosecha
            var NuevaCosecha = await _cosechaService.CrearCosecha(usuarioId, cosechaCreateDto);

            return Ok(NuevaCosecha);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CosechaReadDto>>> VerTodasLasCosechas()
        {

            //le asignamos el claim a la variable usuarioId
            int usuarioId = ObtenerUsuarioIdDelToken();

            var cosechas = await _cosechaService.VerCosechasDelUsuario(usuarioId);

            return Ok(cosechas);
        }

        [HttpGet("{cosechaId}")]
        public async Task<ActionResult<CosechaReadDto>> VerCosechaPorId(int cosechaId)
        {

            //le asignamos el claim a la variable usuarioId
            int usuarioId = ObtenerUsuarioIdDelToken();

            var cosecha = await _cosechaService.VerCosechaPorId(usuarioId, cosechaId);

            if(cosecha == null) return NotFound($"No se encontro la cosecha con el ID {cosechaId}");

            return Ok(cosecha);
        }

        [HttpGet("Catalogo")]
        public async Task<ActionResult<IEnumerable<CosechaReadDto>>> CatalogoDeCosechas()
        {
            var catalogoCosechas = await _cosechaService.CatalogoDeCosechas();

            if (catalogoCosechas == null || !catalogoCosechas.Any()) return NotFound("No se encontraron Cosechas Disponibles");

            return Ok(catalogoCosechas);
        }

        [HttpPut("{cosechaId}")]
        public async Task<ActionResult<CosechaReadDto>> ActualizarCosecha(int cosechaId, CosechaUpdateDto cosechaUpdateDto)
        {

            //le asignamos el claim a la variable usuarioId
            int usuarioId = ObtenerUsuarioIdDelToken();

            var cosecha = await _cosechaService.ActualizarCosecha(usuarioId, cosechaId, cosechaUpdateDto);

            if (cosecha == null) return NotFound($"No se encontro la cosecha con el ID {cosechaId}");

            return Ok(cosecha);
        }

        [HttpDelete("{cosechaId}")]
        public async Task<ActionResult<bool>> EliminarCosecha(int cosechaId)
        {

            //le asignamos el claim a la variable usuarioId
            int usuarioId = ObtenerUsuarioIdDelToken();

            var cosechaEliminar = await _cosechaService.EliminarCosecha(usuarioId, cosechaId);

            if (!cosechaEliminar) return NotFound($"No se encontro la cosecha con el ID {cosechaId}");

            return NoContent();
        }

    }
}
