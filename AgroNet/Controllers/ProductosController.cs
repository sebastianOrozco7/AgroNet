using AgroNet.DTOs.CosechaDto;
using AgroNet.DTOs.ProductoDto;
using AgroNet.Interfaces.Producto;
using AgroNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AgroNet.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductosController : ControllerBase
    {
       private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoReadDto>>> VerTodasLosProductos()
        {
            var Productos = await _productoService.VerProductos();

            return Ok(Productos);
        }
    }
}
