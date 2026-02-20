namespace AgroNet.Models
{
    public class DetallePedido
    {
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        public int IdCosecha { get; set; }
        public decimal CantidadComprada { get; set; }
        public decimal PrecioUnitario { get; set; }

        //conexiones
        public virtual Pedido Pedido { get; set; }
        public virtual Cosecha Cosecha { get; set; } 
    }
}
