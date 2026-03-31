using AgroNet.Data;
using AgroNet.DTOs.PedidoDto;
using AgroNet.Interfaces.Pedido;
using AgroNet.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;



namespace AgroNet.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public PedidoService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<PedidoReadDto> CrearPedido(int usuarioId, PedidoCreateDto pedidoCreateDto)
        {
            //valido la cosecha
            var cosechaValidada = await ValidarCosechayStock(pedidoCreateDto.IdCosecha, pedidoCreateDto.CantidadSolicitada);

            //preparo el pedido
            var NuevoPedido = PrepararPedido(usuarioId, pedidoCreateDto, cosechaValidada);

            //agrego a la base de datos el pedido
            _appDbContext.Pedidos.Add(NuevoPedido);
            await _appDbContext.SaveChangesAsync();

            //retorno el pedido mapeado
            return await ObtenerPedidoMapeado(NuevoPedido.PedidoId);
        }

        private async Task<Cosecha> ValidarCosechayStock(int cosechaId, decimal cantidadSolicitada)
        {
            //valido que la cosecha exista
            var CosechaValidada = await _appDbContext.Cosechas
                .FirstOrDefaultAsync(c => c.CosechaId == cosechaId);

            if (CosechaValidada == null)
                throw new Exception("La cosecha no existe");

            //valido que tenga el stock suficiente para el pedido
            if (CosechaValidada.CantidadDisponible < cantidadSolicitada)
                throw new Exception("La cosecha no cuenta con la cantidad solicitada disponible");

            return CosechaValidada;
        }

        private Pedido PrepararPedido(int usuarioCompradoId, PedidoCreateDto pedidoCreateDto, Cosecha cosecha)
        {
            //mapeo del dto de creacion a la entidad pedido
            var NuevoPedido = _mapper.Map<Pedido>(pedidoCreateDto);
            //asigno a la entidad principal el id del usuario que va a realizar la compra
            NuevoPedido.IdUsuario = usuarioCompradoId;
            //realizo la operacion para calcular el precio total a pagar y se la asigno a el atributo de la entidad principal
            NuevoPedido.TotalPagar = pedidoCreateDto.CantidadSolicitada * cosecha.PrecioPorUnidad;

            return NuevoPedido;
        }

        private void DescontarCantidadDisponibleCosecha(Cosecha cosecha, decimal cantidadSolicitada)
        {
            cosecha.CantidadDisponible -= cantidadSolicitada;
        }

        private async Task<PedidoReadDto> ObtenerPedidoMapeado(int pedidoId)
        {
            var NuevoPedido = await _appDbContext.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Cosecha)
                    .ThenInclude(c => c.Producto)
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId);

            return _mapper.Map<PedidoReadDto>(NuevoPedido);

        }
    }
}
