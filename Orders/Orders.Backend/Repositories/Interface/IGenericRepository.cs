namespace Orders.Backend.Repositories.Interface;

using Orders.Shared.Responses;

public interface IGenericRepository<T> where T : class // Interfaz genérica para repositorios.
{
    Task<ActionResponse<T>> GetAsync(int id); // Obtiene una entidad por su Id.

    Task<ActionResponse<IEnumerable<T>>> GetAsync(); // Obtiene todas las entidades.

    Task<ActionResponse<T>> AddAsync(T entity); // Agrega una nueva entidad.

    Task<ActionResponse<T>> DeleteAsync(int id); // Elimina una entidad por su Id.

    Task<ActionResponse<T>> UpdateAsync(T entity); // Actualizar una entidad existente.
}