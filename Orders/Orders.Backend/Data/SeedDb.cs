using Orders.Shared.Entities;

namespace Orders.Backend.Data;

public class SeedDb //Clase para inicializar la base de datos con datos predeterminados.
                    // Para ello se crea una instancia de esta clase en Program.cs
                    // y se llama al método SeedAsync().
{
    private readonly DataContext _context; //Contexto de la base de datos.

    public SeedDb(DataContext context) //Constructor que recibe el contexto de la base de datos, inyección de dependencia.
    {
        _context = context; //Asignar el contexto.
    }

    public async Task SeedAsync() //Método para inicializar la base de datos.
    {
        await _context.Database.EnsureCreatedAsync(); //Crear la base de datos si no existe.
        await CheckContriesAsync(); //Verificar si hay países.
        await CheckCategoriesAsync(); //Verificar si hay categorías.
    }

    private async Task CheckCategoriesAsync() //Verificar si hay categorías.
    {
        if (!_context.Categories.Any()) //Si no hay categorías..
        {
            _context.Categories.Add(new Category { Name = "Calzado" }); //Agregar categoría "Calzado".
            _context.Categories.Add(new Category { Name = "Tecnología" }); //Agregar categoría "Tecnología".
            await _context.SaveChangesAsync(); //Guardar los cambios en la base de datos.
        }
    }

    private async Task CheckContriesAsync() //Verificar si hay países.
    {
        if (!_context.Countries.Any()) //Si no hay países..
        {
            _context.Countries.Add(new Country { Name = "Colombia" }); //Agregar país "Colombia".
            _context.Countries.Add(new Country { Name = "Estados Unidos" }); //Agregar país "Estados Unidos".
            _context.Countries.Add(new Country { Name = "México" }); //Agregar país "México".
            await _context.SaveChangesAsync(); //Guardar los cambios en la base de datos.
        }
    }
}