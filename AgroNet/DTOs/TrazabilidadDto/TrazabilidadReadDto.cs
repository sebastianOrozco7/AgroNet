using AgroNet.Models;

namespace AgroNet.DTOs.TrazabilidadDto
{
    public class TrazabilidadReadDto
    {
        public int TrazabilidadId { get; set; }
        public int IdPedido { get; set; }
        public EstadoPedido EstadoAnterior { get; set; }
        public EstadoPedido EstadoNuevo { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string Observacion { get; set; }
    }
}
