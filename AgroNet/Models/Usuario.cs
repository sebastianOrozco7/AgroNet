namespace AgroNet.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public int IdRol {  get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        //Llaves foraneas y conexiones
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Finca> Fincas { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }

    }
}
