using System.Security.Claims;
using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories;
using Alarm_Project.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using NuGet.Protocol;

namespace Alarm_Project.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        [HttpPost]
        [Authorize]
        [Route("AddProduct")]
        public async Task<IActionResult> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            // var identity = HttpContext.User.Identity as ClaimsIdentity;
            // var user = User.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.NameId)?.Value;
            // var userIddd = identity?.FindFirst(JwtRegisteredClaimNames.NameId)?.Value;
            
            // await repositoryContext.SaveChangesAsync();
            return Ok(await _productService.AddProductAsync(productCreateDto));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductAsync());
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAllProducts()
        {
            return Ok(await _productService.DeleteAllProductsAsync());
        }
    }
}
