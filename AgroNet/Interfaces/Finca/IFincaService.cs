using AgroNet.DTOs.FincasDto;

namespace AgroNet.Interfaces.Finca
{
    public interface IFincaService
    {

        //Crear
        Task<FincaReadDto> CrearFinca(int UsuarioId, FincaCreateDto fincaCreateDto);

        //Leer
        Task<IEnumerable<FincaReadDto>> VerFincasDelUsuario(int UsuarioId);
        Task<FincaReadDto> VerFincaPorId(int UsuarioId, int FincaId);

        //Actualizar
        Task<FincaReadDto> ActualizarFinca(int FincaId, int UsuarioId, FincaUpdateDto fincaUpdateDto);

        //Eliminar
        Task<bool> EliminarFinca(int FincaId, int UsuarioId);

    }
}
