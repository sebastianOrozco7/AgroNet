using AgroNet.DTOs.TrazabilidadDto;

namespace AgroNet.Interfaces.Trazabilidad
{
    public interface ITrazabilidadService
    {

        //Crear
        public Task<TrazabilidadReadDto> CrearTrazabilidad(int usuarioId, TrazabilidadCreateDto trazabilidadCreateDto);

        //Leer
        public Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesComprador(int usuarioId);
        public Task<IEnumerable<TrazabilidadReadDto>> VerTrazabilidadesAgricultor(int usuarioId);

       
    }
}
