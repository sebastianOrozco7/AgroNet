using AgroNet.DTOs.CosechaDto;
using AgroNet.DTOs.PedidoDto;

namespace AgroNet.Interfaces.Pedido
{
    public interface IPedidoService
    {
        //Crear
        public Task<PedidoReadDto> CrearPedido(int usuarioId, PedidoCreateDto pedidoCreateDto);

    }
}
