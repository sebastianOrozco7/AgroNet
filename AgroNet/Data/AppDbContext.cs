using Microsoft.EntityFrameworkCore;
using AgroNet.Models;

namespace AgroNet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Tablas de la base de datos

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Finca> Fincas { get; set; }
        public DbSet<Cosecha> Cosechas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Trazabilidad> Trazabilidad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //relacion Usuario --> Rol
            
                

        }
    }
}
