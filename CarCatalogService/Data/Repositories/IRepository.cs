using System.Threading.Tasks;

namespace CarCatalogService.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(long id);
    Task CreateAsync(long id, T item);
    Task UpdateAsync(long id, T item);
    Task DeleteAsync(long id);
}
