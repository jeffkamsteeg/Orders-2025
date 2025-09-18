using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Implementations;
using Orders.Backend.Repositories.Interface;
using Orders.Backend.UnitsOfWork;
using Orders.Backend.UnitsOfWork.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection")); // Configuración de la cadena de conexión a SQL Server desde appsettings.json
builder.Services.AddTransient<SeedDb>(); // Inyección de dependencia para la clase SeedDb

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

SeedData(app); // Llamada al método para inicializar la base de datos

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>(); // Obtener el servicio de creación de ámbitos, los cuales
    //permiten gestionar el ciclo de vida de los servicios para que se ha configurado un ámbito (scoped).

    using var scope = scopedFactory!.CreateScope(); // Crear un nuevo ámbito de servicio para la inicialización de datos.
    var service = scope.ServiceProvider.GetService<SeedDb>(); // Obtener una instancia de la clase SeedDb desde el proveedor de servicios del ámbito.
    service!.SeedAsync().Wait(); // Llamar al método SeedAsync para inicializar la base de datos y esperar a que complete.
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();