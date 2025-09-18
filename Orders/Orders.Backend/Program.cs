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
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection")); // Configuraci�n de la cadena de conexi�n a SQL Server desde appsettings.json
builder.Services.AddTransient<SeedDb>(); // Inyecci�n de dependencia para la clase SeedDb

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

SeedData(app); // Llamada al m�todo para inicializar la base de datos

void SeedData(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>(); // Obtener el servicio de creaci�n de �mbitos, los cuales
    //permiten gestionar el ciclo de vida de los servicios para que se ha configurado un �mbito (scoped).

    using var scope = scopedFactory!.CreateScope(); // Crear un nuevo �mbito de servicio para la inicializaci�n de datos.
    var service = scope.ServiceProvider.GetService<SeedDb>(); // Obtener una instancia de la clase SeedDb desde el proveedor de servicios del �mbito.
    service!.SeedAsync().Wait(); // Llamar al m�todo SeedAsync para inicializar la base de datos y esperar a que complete.
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