using Webshop.Entities;

namespace Webshop.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product?> GetProductByProductNumberAsync(string number);
    Task<Product?> GetProductByNameAsync(string name);
    Task CreateProductAsync(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    Task<bool> SaveChangesAsync();
}