using AgroNet.DTOs.PedidoDto;
using AgroNet.DTOs.TrazabilidadDto;
using AgroNet.Interfaces.Pedido;
using AgroNet.Interfaces.Trazabilidad;
using AgroNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace AgroNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrazabilidadController : ControllerBase
    {
        private readonly ITrazabilidadService _trazabilidadService;

        public TrazabilidadController(ITrazabilidadService tazabilidadService)
        {
            _trazabilidadService = tazabilidadService;
        }    

        //metodo para refactorizar el codigo y obtener el token
        private int ObtenerUsuarioIdDelToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(claim.Value);
        }

        [HttpPost]
        public async Task<ActionResult<TrazabilidadReadDto>> RegistrarTrazabilidad (TrazabilidadCreateDto trazabilidadCreateDto)
        {
            int usuarioId = ObtenerUsuarioIdDelToken();

            var Trazabilidad = await _trazabilidadService.CrearTrazabilidad(usuarioId, trazabilidadCreateDto);

            return Ok(Trazabilidad);
        }

       

    }
}
