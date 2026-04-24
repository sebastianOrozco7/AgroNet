using AgroNet.Data;
using AgroNet.DTOs.ProductoDto;
using AgroNet.Interfaces.Producto;
using AgroNet.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AgroNet.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public ProductoService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<ProductoReadDto>> VerProductos()
        {
            var productos = await _appDbContext.Productos
                .Include(p => p.CategoriaProducto)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProductoReadDto>>(productos);
        }
    }
}
