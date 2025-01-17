using BusReservationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BusReservationApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Ruta> Rutas { get; set; }
}