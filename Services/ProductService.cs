using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories;
using Alarm_Project.Services.Contracts;
using AutoMapper;

namespace Alarm_Project.Services;

public class ProductService(ProductRepository productRepository, IMapper _mapper) : IProductService
{

    public async Task<ProductCreateDto> AddProductAsync(ProductCreateDto productCreateDto)
    {
        var map = _mapper.Map<Products>(productCreateDto);
        var first = await productRepository.AddProductAsync(map);
        var final = _mapper.Map<ProductCreateDto>(first);
        return final;
    }

    public async Task<IEnumerable<Products>> GetAllProductAsync()
    {
        return await productRepository.GetAllProductAsync();
    }

    public Task<Products> GetProductByIdAsync()
    {
        throw new Exception();
    }

    public async Task<bool> DeleteAllProductsAsync()
    {
        return await productRepository.DeleteAllProductsAsync();
    }

    public Task<bool> DeleteProductByIdAsync()
    {
        throw new Exception();
    }
}