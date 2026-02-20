namespace AgroNet.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaPedido { get; set; } = DateTime.Now;
        public decimal TotalPagar { get; set; }
        public string EstadoPedido { get; set; }

        //conexiones
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
    }
}
