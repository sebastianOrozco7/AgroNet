using AgroNet.Data;
using AgroNet.DTOs.CosechaDto;
using AgroNet.Interfaces.Cosecha;
using AgroNet.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;



namespace AgroNet.Services
{
    public class CosechaService : ICosechaService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        public CosechaService(IMapper mapper, AppDbContext appDbContext )
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<CosechaReadDto> CrearCosecha(int usuarioId, CosechaCreateDto cosechaCreateDto)
        {
            //primero verifico que la finca exista y que sea del usuario que hace la peticion
            var fincaValida = await _appDbContext.Fincas
                .AnyAsync(f => f.FincaId == cosechaCreateDto.IdFinca && f.IdUsuario == usuarioId);

            // en caso de que no exista la finca o que no sea de ese usuario entraria a este if y mostraria el mensaje de error
            if(!fincaValida)
                throw new Exception("La finca no existe o no tienes permisos sobre ella.");

            //si pasa la validacion entonces sigue con el proceso
            var nuevaCosecha =  _mapper.Map<Cosecha>(cosechaCreateDto);
            _appDbContext.Cosechas.Add(nuevaCosecha);
            await _appDbContext.SaveChangesAsync();

            // Cargamos los datos de la finca y del producto
            // para que la magia de AutoMapper (NombreFinca, NombreProducto) funcione en el return
            await _appDbContext.Entry(nuevaCosecha).Reference(c => c.Finca).LoadAsync();
            await _appDbContext.Entry(nuevaCosecha).Reference(c => c.Producto).LoadAsync();

            //retorno la cosecha pero con la plantilla del dto
            return _mapper.Map<CosechaReadDto>(nuevaCosecha);
        }

        public async Task<IEnumerable<CosechaReadDto>> VerCosechasDelUsuario(int usuarioId)
        {

            var cosechas = await _appDbContext.Cosechas
                .Include(c => c.Finca) // incluyo la finca y el producto para que el dto saque de ahi los nombres
                .Include(c => c.Producto)
                .Where(c => c.Finca.IdUsuario == usuarioId)
                .ToListAsync();

            //mapeamos la entidad completa cosechas para convertirla en la entidad DTO cosehasDto
            return _mapper.Map<IEnumerable<CosechaReadDto>>(cosechas);
        }
        public async Task<CosechaReadDto> VerCosechaPorId(int usuarioId, int cosechaId)
        {
            var cosecha = await _appDbContext.Cosechas
                .Include(c => c.Finca) // incluyo la finca y el producto para que el dto saque de ahi los nombres
                .Include(c => c.Producto)
                .Where(c => c.Finca.IdUsuario == usuarioId && c.CosechaId == cosechaId)
                .FirstOrDefaultAsync();

            if (cosecha == null) return null;

            return _mapper.Map<CosechaReadDto>(cosecha);
        }

        public async Task<IEnumerable<CosechaReadDto>> CatalogoDeCosechas()
        {
            //esta consulta me traera las cosechas disponibles, en crecimiento y con una cantidad disponible mayor a 0;
            var catalogoCosechas = await _appDbContext.Cosechas
                .Include(c => c.Finca)
                .Include(c => c.Producto)
                .Where(c => (c.Estado == EstadoCosecha.Disponible || c.Estado == EstadoCosecha.EnCrecimiento) && c.CantidadDisponible > 0)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CosechaReadDto>>(catalogoCosechas);
        }

        public async Task<CosechaReadDto> ActualizarCosecha(int usuarioId, int cosechaId, CosechaUpdateDto cosechaUpdateDto)
        {
            var cosechaExistente = await _appDbContext.Cosechas
               .Include(c => c.Finca) // incluyo la finca y el producto para que el dto saque de ahi los nombres
               .Include(c => c.Producto)
               .Where(c => c.Finca.IdUsuario == usuarioId && c.CosechaId == cosechaId)
               .FirstOrDefaultAsync();

            if (cosechaExistente == null) return null;

            _mapper.Map(cosechaUpdateDto,cosechaExistente);
            await _appDbContext.SaveChangesAsync();

            await _appDbContext.Entry(cosechaExistente).Reference(c => c.Finca).LoadAsync();
            await _appDbContext.Entry(cosechaExistente).Reference(c => c.Producto).LoadAsync();

            return _mapper.Map<CosechaReadDto>(cosechaExistente);
        }

        public async Task<bool> EliminarCosecha(int usuarioId, int cosechaId)
        {
            //busco la cosecha incluyendo la finca a la que pertenece para saber de que usuario es
            var cosecha = await _appDbContext.Cosechas
                .Include(c => c.Finca)
                .FirstOrDefaultAsync(c => c.CosechaId == cosechaId && c.Finca.IdUsuario == usuarioId);

            //en caso de que la cosecha no exista o no sea del usuario con esa finca
            if(cosecha == null) return false;

            _appDbContext.Cosechas.Remove(cosecha);
            await _appDbContext.SaveChangesAsync();

            return true;

        }
    }
}
