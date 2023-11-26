namespace CarCatalogService.DAL.Repositories.Interfaces;

/// <summary>
/// Represents a generic repository interface for CRUD operations on entities of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of entities handled by the repository.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    ///     Retrieves all entities of type <typeparamref name="T"/> asynchronously.
    /// </summary>
    /// <returns>
    ///     A task representing the asynchronous operation that returns a collection of entities.
    /// </returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    ///     Retrieves a specific entity of type <typeparamref name="T"/> by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity to retrieve.</param>
    /// <returns>
    ///     A task representing the asynchronous operation that returns the entity, or null if not found.
    /// </returns>
    Task<T?> GetAsync(long id);

    /// <summary>
    ///     Creates a new entity of type <typeparamref name="T"/> asynchronously.
    /// </summary>
    /// <param name="item">The entity to create.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateAsync(T item);

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="T"/> asynchronously.
    /// </summary>
    /// <param name="item">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(T item);

    /// <summary>
    /// Deletes an entity of type <typeparamref name="T"/> by its identifier asynchronously.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <returns>
    ///     A task representing the asynchronous operation.
    ///     The result is a <see cref="bool"/> indicating whether the deletion was successful (true) or not (false).
    /// </returns>
    Task<bool> DeleteAsync(long id);
}
