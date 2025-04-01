using AutoMapper;
using Webshop.Interfaces;
using Webshop.Repositories;
using Webshop.Shared.DTOs;
using static MudBlazor.CategoryTypes;

namespace Webshop.Services;

public class ProductCategoriesService : IProductCategoriesService
{
    private readonly IProductCategoriesRepository _productCategoriesRepository;
    private readonly IMapper _mapper;

    public ProductCategoriesService(IProductCategoriesRepository productCategoriesRepository, IMapper mapper)
    {
        _productCategoriesRepository = productCategoriesRepository
        ?? throw new ArgumentNullException(nameof(productCategoriesRepository));
        _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<ProductCategoriesDto>> GetProductCategoriesAsync()
    {
        var productCategories = await _productCategoriesRepository.GetProductCategoriesAsync();
        return _mapper.Map<IEnumerable<ProductCategoriesDto>>(productCategories);
    }
}
