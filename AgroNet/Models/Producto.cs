namespace AgroNet.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string UnidadMedida { get; set; }

        //Conexiones
        public virtual ICollection<Cosecha> Cosechas { get; set; }

    }
}
