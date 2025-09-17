using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.UnitsOfWork;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers;

[ApiController] // Indica que es un controlador de API.
[Route("api/[controller]")] // Ruta del controlador.
public class CountriesController : GenericController<Country> // Controlador genérico para la entidad Country.
{
    public CountriesController(IGenericUnitOfWork<Country> unitOfWork) : base(unitOfWork) // Constructor que recibe una unidad de trabajo genérica para Country.
    {
    }
}