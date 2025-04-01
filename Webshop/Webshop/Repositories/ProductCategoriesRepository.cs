using Microsoft.EntityFrameworkCore;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Repositories;

public class ProductCategoriesRepository : IProductCategoriesRepository
{
    private readonly FlowerboutiqueContext _context;

    public ProductCategoriesRepository(FlowerboutiqueContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync()
        => await _context.ProductCategories.ToListAsync();
}
