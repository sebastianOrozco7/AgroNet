using AgroNet.DTOs.CosechaDto;

namespace AgroNet.Interfaces.Cosecha
{
    public interface ICosechaService
    {

        //Crear
        Task<CosechaReadDto> CrearCosecha(int usuarioId, CosechaCreateDto cosechaCreateDto);

        //Leer
        Task<IEnumerable<CosechaReadDto>> VerCosechasDelUsuario(int usuarioId);
        Task<CosechaReadDto> VerCosechaPorId(int usuarioId, int cosechaId);
        Task<IEnumerable<CosechaReadDto>> VerCosechaPorProducto(int usuarioId, string? NombreProducto);
        Task<IEnumerable<CosechaReadDto>> CatalogoDeCosechas(string? producto, string? municipio);

        //actualizar
        Task<CosechaReadDto> ActualizarCosecha (int  usuarioId, int cosechaId, CosechaUpdateDto cosechaUpdateDto);

        //Eliminar
        Task<bool> EliminarCosecha(int usuarioId, int cosechaId);
    }
}
