using Webshop.Entities;

namespace Webshop.Interfaces;

public interface IProductCategoriesRepository
{
    Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync();
}
