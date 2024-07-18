using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Alarm_Project.Models;
using Alarm_Project.Repositories.Contracts;
using Alarm_Project.Repositories.DbRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Alarm_Project.Repositories;

public class ProductRepository(RepositoryContext repositoryContext, Users users, IHttpContextAccessor httpContextAccessor) : IProductRepository<Products>
{

    public async Task<Products> AddProductAsync(Products products)
    {
        var existProduct =
            await repositoryContext.Products.FirstOrDefaultAsync(p => p.ProductName == products.ProductName);
        if (existProduct != null)
        {
            throw new ArgumentException("Product already exist");
        }

        var identity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var productIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
        var productUserId = new Guid(productIdClaim.Value);
        products.UserId = productUserId;

        await repositoryContext.AddAsync(products);
        await repositoryContext.SaveChangesAsync();
        return products;
    }

    public async Task<IEnumerable<Products>> GetAllProductAsync()
    {
        var allProducts = await repositoryContext.Products.ToListAsync();
        if (allProducts.Count == 0)
        {
            throw new ArgumentNullException("No products");
        }
        return allProducts.ToList();
    }

    public Task<Products> GetProductByIdAsync()
    {
        throw new Exception();
    }

    public async Task<bool> DeleteAllProductsAsync()
    {
        var allProducts = await repositoryContext.Products.ToListAsync();
        repositoryContext.Products.RemoveRange(allProducts);
        await repositoryContext.SaveChangesAsync();
        return true;
    }

    public Task<bool> DeleteProductByIdAsync()
    {
        throw new Exception();
    }
}