using Alarm_Project.DTOs;

namespace Alarm_Project.Repositories.Contracts;

public interface IProductRepository<T>
{
    Task<T> AddProductAsync(T entity);
    Task<IEnumerable<T>> GetAllProductAsync();
    Task<T> GetProductByIdAsync();
    Task<bool> DeleteAllProductsAsync();
    Task<bool> DeleteProductByIdAsync();
}