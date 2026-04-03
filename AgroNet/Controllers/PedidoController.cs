using AgroNet.DTOs.PedidoDto;
using AgroNet.Interfaces.Pedido;
using AgroNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgroNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
       private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        //metodo para refactorizar el codigo y obtener el token
        private int ObtenerUsuarioIdDelToken()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(claim.Value);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoReadDto>> RegistrarPedido(PedidoCreateDto pedidoCreateDto)
        {
            //le asigno el claim a la variable usuarioId
            int usuarioId = ObtenerUsuarioIdDelToken();

            var NuevoPedido = await _pedidoService.CrearPedido(usuarioId,pedidoCreateDto);

            return Ok(NuevoPedido);
        }

        [HttpPatch("{pedidoId}/estado")]
        public async Task<ActionResult<PedidoReadDto>> ModificarEstadoPedido(int pedidoId, [FromBody]EstadoPedido estado)
        {
            int usuarioId = ObtenerUsuarioIdDelToken();

            var PedidoActualizado = await _pedidoService.ModificarEstadoPedido(pedidoId,usuarioId,estado);

            return Ok(PedidoActualizado);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoReadDto>>> VerPedidosDelComprador(
            [FromQuery] string? Estado)
        {
            int usuarioId = ObtenerUsuarioIdDelToken();

            var PedidosComprador = await _pedidoService.VerPedidosComprador(usuarioId,Estado);

            if(PedidosComprador == null || !PedidosComprador.Any()) return NotFound("No se encontraron Pedidos"); // primero verifica si la lista existe (si no es null) y despues con .any() verifica si la lista no tiene elementos

            return Ok(PedidosComprador);
        }
    }
}
