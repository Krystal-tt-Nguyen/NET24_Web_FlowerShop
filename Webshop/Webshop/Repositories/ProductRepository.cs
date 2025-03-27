using Microsoft.EntityFrameworkCore;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly FlowerboutiqueContext _context;

    public ProductRepository(FlowerboutiqueContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Product>> GetProductsAsync() 
        => await _context.Products
                    .Include(c => c.ProductCategory)
                    .ToListAsync();

    public async Task<Product?> GetProductByIdAsync(int id)  
        => await _context.Products
                    .Include(p => p.ProductCategory)
                    .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Product?> GetProductByProductNumberAsync(string number) 
        => await _context.Products.FirstOrDefaultAsync(p => p.ProductNumber == number);

    public async Task<Product?> GetProductByNameAsync(string name) 
        => await _context.Products.FirstOrDefaultAsync(p => p.ProductName == name);

    public async Task CreateProductAsync(Product product) 
        => await _context.AddAsync(product);

    public void UpdateProduct(Product product) 
        => _context.Update(product);

    public void DeleteProduct(Product product) 
        => _context.Remove(product);
    
    public async Task<bool> SaveChangesAsync()
        => await _context.SaveChangesAsync() >= 0;

}
