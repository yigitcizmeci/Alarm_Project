using Alarm_Project.DTOs;
using Alarm_Project.Models;

namespace Alarm_Project.Services.Contracts;

public interface IProductService
{
    Task<ProductCreateDto> AddProductAsync(ProductCreateDto productCreateDto);
    Task<IEnumerable<Products>> GetAllProductAsync();
    Task<Products> GetProductByIdAsync();
    Task<bool> DeleteAllProductsAsync();
    Task<bool> DeleteProductByIdAsync();
}