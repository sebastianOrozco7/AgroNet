using AgroNet.Models;

namespace AgroNet.DTOs.PedidoDto
{
    public class PedidoReadDto
    {
        public int PedidoId { get; set; }
        public string UsuarioNombre { get; set; }
        public string ProductoNombre { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public string Estado { get; set; } 
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public decimal TotalPagar { get; set; }
    }
}
