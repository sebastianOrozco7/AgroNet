namespace AgroNet.Models
{
    public class Rol
    {
        public int RolId { get; set; }
        public string NombreRol { get; set; }

        //conexiones
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
