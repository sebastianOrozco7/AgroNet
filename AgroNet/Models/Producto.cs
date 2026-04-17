namespace AgroNet.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int IdCategoria { get; set; }
        public string UnidadMedida { get; set; }

        //Conexiones
        public virtual ICollection<Cosecha> Cosechas { get; set; }
        public virtual CategoriaProducto CategoriaProducto { get; set; }

    }
}
