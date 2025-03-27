using Webshop.Shared.DTOs;

namespace Webshop.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProductsAsync();
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<ProductDto> GetProductByNameAsync(string name);
    Task<ProductDto> CreateProductAsync(CreateProductDto newProduct);
    Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updatedProduct);
    Task<bool> DeleteProductAsync(int id);
}
