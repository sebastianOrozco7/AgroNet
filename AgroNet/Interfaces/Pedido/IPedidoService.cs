using AgroNet.DTOs.CosechaDto;
using AgroNet.DTOs.PedidoDto;
using AgroNet.DTOs.TrazabilidadDto;
using AgroNet.Models;

namespace AgroNet.Interfaces.Pedido
{
    public interface IPedidoService
    {
        //Crear
        Task<PedidoReadDto> CrearPedido(int usuarioId, PedidoCreateDto pedidoCreateDto);

        //Actualizar
        Task<PedidoReadDto> ModificarEstadoPedido(int pedidoId, int usuarioAgricultorId, EstadoPedido estadoPedido);
        Task CancelarPedido(int usuarioId, int pedidoId);

        //Leer
        Task<IEnumerable<PedidoReadDto>> VerPedidosComprador(int usuarioId, string? estado);
        Task<IEnumerable<PedidoReadDto>> VerPedidosAgricultor(int usuarioId, string? estado);
    }
}
