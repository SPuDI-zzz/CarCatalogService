namespace CarCatalogService.DAL.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(long id);
    Task CreateAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(long id);
}
