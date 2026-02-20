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

            //relacion Rol --> Usuario
            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Usuarios)
                .WithOne(u => u.Rol)
                .HasForeignKey(u => u.IdRol);

            //Relacion usuario --> Finca
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Fincas)
                .WithOne(f => f.Usuario)
                .HasForeignKey(f => f.IdUsuario);

            //Relacion Finca --> Cosecha
            modelBuilder.Entity<Finca>()
                .HasMany(f => f.Cosechas)
                .WithOne(c => c.Finca)
                .HasForeignKey(c => c.IdFinca);

            //Relacion Producto --> Cosecha
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Cosechas)
                .WithOne(c => c.Producto)
                .HasForeignKey(c => c.IdProducto);

            //Relacion Usuario --> Pedido
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Pedidos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.IdUsuario);

            //Relacion Pedido --> DetallePedido
            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.DetallePedidos)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.IdDetallePedido);

            //Relacion Cosecha --> DetallePedido
            modelBuilder.Entity<Cosecha>()
                .HasMany(c => c.DetallePedidos)
                .WithOne(d => d.Cosecha)
                .HasForeignKey(d => d.IdDetallePedido);

            //Relacion Cosecha --> Trazabilidades
            modelBuilder.Entity<Cosecha>()
                .HasMany(c => c.Trazabilidades)
                .WithOne(t => t.Cosecha)
                .HasForeignKey(t => t.IdTrazabilidad);


            // --- CONFIGURACIÓN DE DECIMALES (Precisión) ---

            // Finca: Ubicación GPS exacta
            modelBuilder.Entity<Finca>().Property(f => f.Latitud).HasPrecision(18, 10);
            modelBuilder.Entity<Finca>().Property(f => f.Longitud).HasPrecision(18, 10);

            // Cosecha: Cantidad de kilos y Precio actual
            modelBuilder.Entity<Cosecha>().Property(c => c.CantidadDisponible).HasPrecision(10, 2);
            modelBuilder.Entity<Cosecha>().Property(c => c.PrecioPorUnidad).HasPrecision(10, 2);

            // Pedido: Total de la factura
            modelBuilder.Entity<Pedido>().Property(p => p.TotalPagar).HasPrecision(12, 2);

            // DetallePedido: Precio congelado al momento de compra
            modelBuilder.Entity<DetallePedido>().Property(d => d.PrecioUnitario).HasPrecision(10, 2);
            modelBuilder.Entity<DetallePedido>().Property(d => d.CantidadComprada).HasPrecision(10, 2);

        }
    }
}
