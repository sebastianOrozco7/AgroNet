using AgroNet.DTOs.CosechaDto;

namespace AgroNet.Interfaces.Cosecha
{
    public interface ICosechaService
    {

        //Crear
        public Task<CosechaReadDto> CrearCosecha(int usuarioId, CosechaCreateDto cosechaCreateDto);

        //Leer
        public Task<IEnumerable<CosechaReadDto>> VerCosechasDelUsuario(int usuarioId);
        public Task<CosechaReadDto> VerCosechaPorId(int usuarioId, int cosechaId);
        public Task<IEnumerable<CosechaReadDto>> CatalogoDeCosechas();

        //actualizar
        public Task<CosechaReadDto> ActualizarCosecha (int  usuarioId, int cosechaId, CosechaUpdateDto cosechaUpdateDto);

        //Eliminar
        public Task<bool> EliminarCosecha(int usuarioId, int cosechaId);
    }
}
