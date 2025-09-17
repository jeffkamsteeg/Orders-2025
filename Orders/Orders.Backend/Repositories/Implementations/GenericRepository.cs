using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interface;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context; //Contexto de la base de datos.
    private readonly DbSet<T> _entity; //Conjunto de entidades del tipo T.

    public GenericRepository(DataContext context) //Constructor.
    {
        _context = context; //Inyección de dependencia.
        _entity = _context.Set<T>(); //Obtener el conjunto de entidades del tipo T.
    }

    public DataContext Context { get; } //Exponer el contexto.

    public virtual async Task<ActionResponse<T>> AddAsync(T entity) //Agregar una entidad.
    {
        _context.Add(entity); //EF sabe que guardar sin país, si categoría, etc.
        try
        {
            await _context.SaveChangesAsync(); //Guardar en la base de datos.
            return new ActionResponse<T> //Retornar respuesta.
            {
                WasSuccess = true,  //Indicar que fue exitoso.
                Result = entity //Retornar la entidad agregada.
            };
        }
        catch (DbUpdateException) //Capturar errores de actualización de la base de datos.
        {
            return DbUpdateExceptionActionResponse(); //Retornar respuesta de error.
        }
        catch (Exception exception) //Capturar errores.
        {
            return ExceptionActionRespose(exception);
        }
    }

    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity) //Agregar una entidad.
    {
        _context.Update(entity); //EF sabe que actualizar sin país, si categoría, etc.
        try
        {
            await _context.SaveChangesAsync(); //Guardar en la base de datos.
            return new ActionResponse<T> //Retornar respuesta.
            {
                WasSuccess = true,  //Indicar que fue exitoso.
                Result = entity //Retornar la entidad agregada.
            };
        }
        catch (DbUpdateException) //Capturar errores de actualización de la base de datos.
        {
            return DbUpdateExceptionActionResponse(); //Retornar respuesta de error.
        }
        catch (Exception exception) //Capturar errores.
        {
            return ExceptionActionRespose(exception);
        }
    }

    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id); //Buscar la entidad por su Id.
        if (row == null) //Si no se encuentra la entidad.
        {
            return new ActionResponse<T> //Retornar respuesta.
            {
                Message = "Registro no encontrado." //Mensaje genérico.
            };
        }
        try
        {
            await _context.SaveChangesAsync(); //Guardar los cambios en la base de datos.
            return new ActionResponse<T> //Retornar respuesta.
            {
                WasSuccess = true, //Indicar que fue exitoso.
            };
        }
        catch
        {
            return new ActionResponse<T> //Retornar respuesta de error genérico.
            {
                Message = "Error, registros relacionados." //Mensaje genérico.
            };
        }
    } //

    public virtual async Task<ActionResponse<T>> GetAsync(int id) //Obtener una entidad por su Id.
    {
        var row = await _entity.FindAsync(id); //Buscar la entidad por su Id.
        if (row == null) //Si no se encuentra la entidad.
        {
            return new ActionResponse<T> //Retornar respuesta.
            {
                Message = "Registro no encontrado." //Mensaje genérico.
            };
        }
        return new ActionResponse<T> //Retornar respuesta.
        {
            WasSuccess = true, //Indicar que fue exitoso.
            Result = row //Retornar la entidad encontrada.
        };
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => new
        ActionResponse<IEnumerable<T>> //Obtener todas las entidades.
    {
        WasSuccess = true,
        Result = await _entity.ToListAsync() //Retornar todas las entidades.
    };

    private ActionResponse<T> ExceptionActionRespose(Exception exception) => new ActionResponse<T> //Retornar respuesta de error genérico.
    {
        Message = exception.Message //Retornar el mensaje de la excepción.
    };

    private ActionResponse<T> DbUpdateExceptionActionResponse() => new ActionResponse<T> //Retornar respuesta de error de actualización de la base de datos.
    {
        Message = "Error al guardar en la base de datos. El registro ya existe." //Mensaje genérico.
    };
}