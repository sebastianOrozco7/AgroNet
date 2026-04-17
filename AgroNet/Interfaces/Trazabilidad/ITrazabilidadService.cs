using AgroNet.DTOs.TrazabilidadDto;

namespace AgroNet.Interfaces.Trazabilidad
{
    public interface ITrazabilidadService
    {

        //Crear
        Task<TrazabilidadReadDto> CrearTrazabilidad(int usuarioId, TrazabilidadCreateDto trazabilidadCreateDto);

        //Leer
        Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesComprador(int usuarioId);
        Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesAgricultor(int usuarioId);

       
    }
}
