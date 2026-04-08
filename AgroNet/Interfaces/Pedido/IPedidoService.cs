using AgroNet.DTOs.CosechaDto;
using AgroNet.DTOs.PedidoDto;
using AgroNet.Models;

namespace AgroNet.Interfaces.Pedido
{
    public interface IPedidoService
    {
        //Crear
        public Task<PedidoReadDto> CrearPedido(int usuarioId, PedidoCreateDto pedidoCreateDto);

        //Actualizar
        public Task<PedidoReadDto> ModificarEstadoPedido(int pedidoId, int usuarioAgricultorId, EstadoPedido estadoPedido);

        //Leer
        public Task<IEnumerable<PedidoReadDto>> VerPedidosComprador(int usuarioId, string? estado);
        public Task<IEnumerable<PedidoReadDto>> VerPedidosAgricultor(int usuarioId, string? estado);
    }
}
