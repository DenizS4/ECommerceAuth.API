using ECommerce.Core.DTO;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController :Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        
        
    }
    [HttpGet("GetProductsByProductId")]
    public async Task<IActionResult> GetProductsByProductId(int productId)
    {
        
        
    }
    [HttpGet("GetProductsByCategoryId")]
    public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
    {
        
        
    }
    [HttpGet("GetProductsBySearchString")]
    public async Task<IActionResult> GetProductsByCategoryId(string searchString)
    {
        
        
    }
    [HttpPut("UpdateProducts")]
    public async Task<IActionResult> UpdateProducts(ProductDto product)
    {
        
        
    }
    [HttpPost("AddProducts")]
    public async Task<IActionResult> AddProducts(ProductDto product)
    {
        
        
    }
    [HttpDelete("DeleteProducts")]
    public async Task<IActionResult> DeleteProducts(ProductDto product)
    {
        
        
    }
    
}