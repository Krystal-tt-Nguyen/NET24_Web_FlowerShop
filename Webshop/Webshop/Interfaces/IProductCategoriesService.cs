using Webshop.Shared.DTOs;

namespace Webshop.Interfaces;

public interface IProductCategoriesService
{
    Task<IEnumerable<ProductCategoriesDto>> GetProductCategoriesAsync();
}
