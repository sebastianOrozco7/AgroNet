namespace AgroNet.Models
{
    public class Cosecha
    {
        public int CosechaId {  get; set; }
        public int IdFinca { get; set; }
        public int IdProducto { get; set; }
        public decimal CantidadDisponible { get; set; }
        public decimal PrecioPorUnidad { get; set; }
        public DateTime FechaEstimada {  get; set; }
        public string Estado { get; set; }

        //Conexiones 
        public virtual Finca Finca { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
        public virtual ICollection<Trazabilidad> Trazabilidades { get; set; }
    }
}
