using Microsoft.EntityFrameworkCore;
using Orders.Shared.Entities;

namespace Orders.Backend.Data;

public class DataContext : DbContext // Contexto de la base de datos.
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) // Constructor que recibe opciones de configuración.
    {
    }

    public DbSet<Category> Categories { get; set; } // DbSet para las categorías.
    public DbSet<Country> Countries { get; set; } // DbSet para los países.

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Configuración del modelo.
    {
        base.OnModelCreating(modelBuilder); // Llamada al método base para configuraciones adicionales.
        modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique(); // Crear un índice único para el nombre de la categoría.
        modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique(); // Crear un índice único para el nombre del país.
    }
}