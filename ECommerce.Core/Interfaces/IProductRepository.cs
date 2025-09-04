using ECommerce.Core.DTO;

namespace ECommerce.Core.Interfaces;

public interface IProductRepository
{
    Task<List<ProductDto>> GetAllProducts();
    Task<ProductDto> GetProductsById(int productId);
    Task<List<ProductDto>> GetProductsByCategoryId(int categoryId);
    Task<ProductDto> CreateProduct(ProductDto product);
    Task<ProductDto> UpdateProduct(ProductDto product);
    Task<bool> DeleteProduct(int productId);
    
}