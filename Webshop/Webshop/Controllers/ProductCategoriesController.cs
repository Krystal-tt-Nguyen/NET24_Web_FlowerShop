using Microsoft.AspNetCore.Mvc;
using Webshop.Interfaces;
using Webshop.Services;
using Webshop.Shared.DTOs;

namespace Webshop.Controllers;

[ApiController]
[Route("api/categories")]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoriesService _productCategoriesService;

    public ProductCategoriesController(IProductCategoriesService productCategoriesService)
    {
        _productCategoriesService = productCategoriesService
            ?? throw new ArgumentNullException(nameof(productCategoriesService));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductCategoriesDto>>> GetProductCategories()
    {
        var categories = await _productCategoriesService.GetProductCategoriesAsync();
        return Ok(categories);
    }
}
