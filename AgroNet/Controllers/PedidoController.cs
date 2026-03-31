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


    }
}
