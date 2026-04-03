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

        //METODOS PRINCIPALES

        public async Task<PedidoReadDto> CrearPedido(int usuarioId, PedidoCreateDto pedidoCreateDto)
        {
            //valido si la cosecha existe
            var cosecha = await ValidarCosechaExistente(pedidoCreateDto.IdCosecha);

            //valido si la cosecha tiene el stock necesario
            var cosechaValidada = await ValidarStockCosecha(cosecha, pedidoCreateDto.CantidadSolicitada);

            //preparo el pedido
            var NuevoPedido = PrepararPedido(usuarioId, pedidoCreateDto, cosechaValidada);

            //agrego a la base de datos el pedido
            _appDbContext.Pedidos.Add(NuevoPedido);
            await _appDbContext.SaveChangesAsync(); // realizo todos los procesos anteriores antes de guardar en la DB (transaccion)

            //retorno el pedido mapeado
            return await ObtenerPedidoMapeado(NuevoPedido.PedidoId);
        }

        public async Task<PedidoReadDto> ModificarEstadoPedido(int pedidoId,int usuarioAgricultorId, EstadoPedido estadoPedido)
        {
            var Pedido = await _appDbContext.Pedidos
                .Include(p => p.Cosecha)
                    .ThenInclude(c => c.Finca)
                .FirstOrDefaultAsync(p => p.PedidoId == pedidoId  && p.Cosecha.Finca.IdUsuario == usuarioAgricultorId); // me va a buscar el pedido que coincida con el id del pedido que digite y con el id del agricultor que tiene la finca de donde viene la cosecha 

            if (Pedido == null)
                throw new Exception("Pedido NO encontrado o permisos denegados");

            //si el agricultor aprueba el pedido entonces descuento del stock la cantidad solicitada
            if (estadoPedido == EstadoPedido.Aprobado)
            {
                var cosecha = await ValidarStockCosecha(Pedido.Cosecha, Pedido.CantidadSolicitada);
                cosecha.CantidadDisponible -= Pedido.CantidadSolicitada;
            }

            //Cambio el estado del pedido
            Pedido.Estado = estadoPedido;
            //Guardo en el DB
            await _appDbContext.SaveChangesAsync();

            //reutilizo el metodo para obtener el mapeo
            return await ObtenerPedidoMapeado(Pedido.PedidoId);
        }

        public async Task<IEnumerable<PedidoReadDto>> VerPedidosComprador(int usuarioId, string? estado)
        {
            //este es el query general
            var Query = _appDbContext.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.Cosecha)
                    .ThenInclude(c => c.Producto)
                .Where(p => p.IdUsuario == usuarioId)
                .AsQueryable();  // Use .AsQueryable() para que no ir directamenta a la base de datos

            //Verifico si el usuario lleno el filtro
            if (!string.IsNullOrWhiteSpace(estado))
            {
                //el filtro es de tipo string pero en la base de datos es de tipo Enum entonces primero debo convertir ese string a un enum para aplicar el filtro
                if(Enum.TryParse<EstadoPedido>(estado, true, out var EstadoEnum)) //aqui estoy convirtiendo el string a enum y el resultado lo devuelvo en la var EstadoEnum
                {
                    //Si la conversion se realizo bien entonces agrego a el Query este filtro de comparacion
                    Query = Query.Where(p => p.Estado == EstadoEnum);
                }
                else
                {
                    throw new Exception("No se encontraron Pedidos");
                }
            }

            //Despues de verificar si el usuario lleno el filtro, ejecutamos la consulta
            var ListaPedidos = await Query.ToListAsync();

            return _mapper.Map<IEnumerable<PedidoReadDto>>(ListaPedidos);
        }


        //METODOS SECUNDARIOS

        private async Task<Cosecha> ValidarCosechaExistente(int cosechaId)
        {
            //valido que la cosecha exista
            var CosechaValidada = await _appDbContext.Cosechas
                .FirstOrDefaultAsync(c => c.CosechaId == cosechaId);

            if (CosechaValidada == null)
                throw new Exception("La cosecha no existe");

            return CosechaValidada;
        }

        private async Task<Cosecha> ValidarStockCosecha(Cosecha cosecha, decimal cantidadSolicitada)
        {
            //valido que tenga el stock suficiente para el pedido
            if (cosecha.CantidadDisponible < cantidadSolicitada)
                throw new Exception("La cosecha no cuenta con la cantidad solicitada disponible");

            return cosecha;
        }

        private Pedido PrepararPedido(int usuarioCompradorId, PedidoCreateDto pedidoCreateDto, Cosecha cosecha)
        {
            //mapeo del dto de creacion a la entidad pedido
            var NuevoPedido = _mapper.Map<Pedido>(pedidoCreateDto);
            //asigno a la entidad principal el id del usuario que va a realizar la compra
            NuevoPedido.IdUsuario = usuarioCompradorId;
            //realizo la operacion para calcular el precio total a pagar y se la asigno a el atributo de la entidad principal
            NuevoPedido.TotalPagar = pedidoCreateDto.CantidadSolicitada * cosecha.PrecioPorUnidad;

            return NuevoPedido;
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
