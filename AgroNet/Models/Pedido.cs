namespace AgroNet.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public int IdUsuario { get; set; }
        public int IdCosecha { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public decimal TotalPagar { get; set; }

        //conexiones
        public virtual Usuario Usuario { get; set; }
        public virtual Cosecha Cosecha { get; set; }
        public virtual ICollection<Trazabilidad> Trazabilidades { get; set; }
    }
}
