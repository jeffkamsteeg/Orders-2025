using Orders.Shared.Responses;

namespace Orders.Backend.UnitsOfWork;

public interface IGenericUnitOfWork<T> where T : class // Representa un tipo generico de clase.
{
    Task<ActionResponse<T>> GetAsync(int id); // Obtener una entidad por su ID.

    Task<ActionResponse<IEnumerable<T>>> GetAsync(); // Obtener todas las entidades.

    Task<ActionResponse<T>> AddAsync(T entity); // Agregar una nueva entidad.

    Task<ActionResponse<T>> DeleteAsync(int id); // Eliminar una entidad por su ID.

    Task<ActionResponse<T>> UpdateAsync(T entity); // Actualizar una entidad existente.
}