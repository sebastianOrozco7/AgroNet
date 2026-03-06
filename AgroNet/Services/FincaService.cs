using AgroNet.Data;
using AgroNet.DTOs.FincasDto;
using AgroNet.Interfaces.Finca;
using AgroNet.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;


namespace AgroNet.Services
{
    public class FincaService : IFincaService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        public FincaService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<FincaReadDto> CrearFinca(int UsuarioId, FincaCreateDto fincaCreateDto)
        {
            //Mapeamos los datos del DTO a la entidad Principal
            var NuevaFinca = _mapper.Map<Finca>(fincaCreateDto);

            //asigno el usuario dueño de la finca, este Id lo conseguimos del claim en el controller
            NuevaFinca.IdUsuario = UsuarioId;

            //agregamos la finca a la base de datos y guardamos
            _appDbContext.Fincas.Add(NuevaFinca);
            await _appDbContext.SaveChangesAsync();

            // Cargamos los datos del usuario 
            // para que la magia de AutoMapper (UsuarioNombre) funcione en el return
            await _appDbContext.Entry(NuevaFinca).Reference(f => f.Usuario).LoadAsync();

            // Convertimos la entidad guardada (ya con su Id) al DTO de lectura y lo devolvemos
            return _mapper.Map<FincaReadDto>(NuevaFinca);
        }

        public async Task<IEnumerable<FincaReadDto>> VerFincasDelUsuario(int UsuarioId)
        {
            //Buscamos en la DB las fincas del usuario y incluimos el usuario para traer la info del usuario

            var Fincas = await _appDbContext.Fincas
                .Include(f => f.Usuario)
                .Where(f => f.IdUsuario == UsuarioId)
                .ToListAsync();

            // retornamos la lista de fincas pero con el mapeo para que solo muestre la info del FincareadDto
            return _mapper.Map<IEnumerable<FincaReadDto>>(Fincas);
        }

        public async Task<FincaReadDto> VerFincaPorId(int UsuarioId, int FincaId)
        {
            var Finca = await _appDbContext.Fincas
                .Include(f => f.Usuario)
                .Where(f => f.IdUsuario == UsuarioId && f.FincaId == FincaId)
                .FirstOrDefaultAsync();

            //en caso de que no encuentre la finca entonces que me retorne null
            if (Finca == null) return null;

            return _mapper.Map<FincaReadDto>(Finca);

                
        }


        public async Task<FincaReadDto> ActualizarFinca(int fincaId, int usuarioId, FincaUpdateDto fincaUpdateDto)
        {
            var FincaExiste = await _appDbContext.Fincas
                .Include(f => f.Usuario)
                .Where(f => f.IdUsuario == usuarioId && f.FincaId == fincaId)
                .FirstOrDefaultAsync();

            if (FincaExiste == null) return null;

            _mapper.Map(fincaUpdateDto, FincaExiste);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<FincaReadDto>(FincaExiste);

        }

        public async Task<bool> EliminarFinca(int fincaId, int usuarioId)
        {
            var FincaExiste = await _appDbContext.Fincas
                .Where(f => f.IdUsuario == usuarioId && f.FincaId == fincaId)
                .FirstOrDefaultAsync();

            if (FincaExiste == null) return false;

            //Eliminamos la finca de la base de datos y guardamos
            _appDbContext.Fincas.Remove(FincaExiste);
            await _appDbContext.SaveChangesAsync();

            return true;

        }
    }
}
