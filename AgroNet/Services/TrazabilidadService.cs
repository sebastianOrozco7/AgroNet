using AgroNet.Data;
using AgroNet.DTOs.TrazabilidadDto;
using AgroNet.Interfaces.Trazabilidad;
using AgroNet.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace AgroNet.Services
{
    public class TrazabilidadService : ITrazabilidadService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public TrazabilidadService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        //METODOS PRINCIPALES
        public async Task<TrazabilidadReadDto> CrearTrazabilidad(int usuarioId, TrazabilidadCreateDto trazabilidadCreateDto)
        {
            //Valido si el pedido existe
            var Pedido = await  ValidarPedidoExistente(trazabilidadCreateDto.IdPedido, usuarioId);

            var NuevaTrazabilidad = PrepararTrazabilidad(Pedido, trazabilidadCreateDto);

            //guardo la trazabilidad
            _appDbContext.Trazabilidad.Add(NuevaTrazabilidad);

            // realizo todos los procesos anteriores antes de guardar en la DB (transaccion)
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<TrazabilidadReadDto>(NuevaTrazabilidad);

        }

        public async Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesComprador(int usuarioId)
        {
            //traigo las trazabilidades del usuario
            var trazabilidades = await _appDbContext.Trazabilidad
                .Where(t => t.Pedido.IdUsuario == usuarioId)
                .ToListAsync();

            //Mapeo
            return _mapper.Map<IEnumerable<TrazabilidadReadDto>>(trazabilidades);
        }

        public async Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesAgricultor(int usuarioId)
        {
            //traigo las trazabilidades del usuario Agricultor
            var trazabilidades = await _appDbContext.Trazabilidad
                .Include(t => t.Pedido)
                    .ThenInclude(p => p.Cosecha)
                        .ThenInclude(c => c.Finca)
                .Where(t => t.Pedido.Cosecha.Finca.IdUsuario == usuarioId)
                .ToListAsync();

            //Mapeo
            return _mapper.Map<IEnumerable<TrazabilidadReadDto>>(trazabilidades);
        }

        //METODOS SECUNDARIOS
        private Trazabilidad PrepararTrazabilidad(Pedido pedido, TrazabilidadCreateDto trazabilidad)
        {
            //Guardo el estado actual del pedido antes de cambiarlo
            var estadoAnterior = pedido.Estado;

            //crear trazabilidad
            var NuevaTrazabilidad = _mapper.Map<Trazabilidad>(trazabilidad);

            // le asigno el estado actual del pedido al estado anterior de la trazabilidad 
            NuevaTrazabilidad.EstadoAnterior = estadoAnterior;

            //Actualizo el estado actual del pedido original
            pedido.Estado = trazabilidad.EstadoNuevo;

            return NuevaTrazabilidad;
        }

        private async Task<Pedido> ValidarPedidoExistente(int pedidoId, int usuarioId)
        {
            var Pedido = await _appDbContext.Pedidos
                .Include(p => p.Cosecha)
                    .ThenInclude(c => c.Finca)
                .FirstOrDefaultAsync(p => p.PedidoId  == pedidoId && p.Cosecha.Finca.IdUsuario == usuarioId);

            if (Pedido == null)
                throw new Exception("El Pedido no existe o no tienes permisos sobre el");

            return Pedido;
        }

        
    }
}
