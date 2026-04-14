using AgroNet.DTOs.TrazabilidadDto;

namespace AgroNet.Interfaces.Trazabilidad
{
    public interface ITrazabilidadService
    {
        public Task<TrazabilidadReadDto> CrearTrazabilidad(int usuarioId, TrazabilidadCreateDto trazabilidadCreateDto);
        public Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesComprador(int usuarioId);
        public Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesAgricultor(int usuarioId);
    }
}
