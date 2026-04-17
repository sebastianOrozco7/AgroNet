namespace AgroNet.Models
{
    public class CategoriaProducto
    {
        public int CategoriaProductoId { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
