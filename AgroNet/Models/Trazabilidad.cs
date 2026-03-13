namespace AgroNet.Models
{
    public class Trazabilidad
    {
        public int TrazabilidadId { get; set; }
        public int IdPedido { get; set; }
        public string EstadoAnterior { get; set; }
        public string EstadoNuevo { get; set; }
        public DateTime FechaCambio { get; set; } = DateTime.Now;
        public string Observacion { get; set; }

        //conexiones 
        public virtual Pedido Pedido { get; set; }
    }
}
