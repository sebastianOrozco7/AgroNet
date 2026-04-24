using AgroNet.DTOs.ProductoDto;

namespace AgroNet.Interfaces.Producto
{
    public interface IProductoService
    {
        //Leer
        Task<IEnumerable<ProductoReadDto>> VerProductos();
    }
}
