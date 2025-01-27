using BusReservationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BusReservationApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Ruta> Rutas { get; set; }
    public DbSet<Boleto> Boletos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Boleto>()
            .HasIndex(b => b.ReservaId)
            .IsUnique(); // Hace que ReservaId sea único
    }
}