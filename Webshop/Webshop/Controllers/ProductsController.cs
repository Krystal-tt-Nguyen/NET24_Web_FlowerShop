using Microsoft.AspNetCore.Mvc;
using Webshop.Interfaces;
using Webshop.Shared.DTOs;

namespace Webshop.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService 
            ?? throw new ArgumentNullException(nameof(productService));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product is null) 
        {
            return NotFound($"No product found with given ID: {id}.");
        }

        return Ok(product);
    }


    [HttpGet("name/{name}")]
    public async Task<ActionResult<ProductDto>> GetProductByName(string name)
    {
        var product = await _productService.GetProductByNameAsync(name);

        if (product is null)
        {
            return NotFound($"No product found with given productname: {name}.");
        }

        return Ok(product);
    }


    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto newProduct) 
    {
        if (newProduct is null)
        {
            return BadRequest("Product data cannot be null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = await _productService.CreateProductAsync(newProduct);

        if (product is null)
        {
            return Conflict("Cannot add product: An product with identical productnumber is already registered.");
        }

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id, UpdateProductDto updatedProduct)
    {
        if (updatedProduct is null)
        {
            return BadRequest("Product data cannot be null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = await _productService.UpdateProductAsync(id, updatedProduct);

        if (product is null)
        {
            return NotFound($"No product found with given ID: {id}.");
        }

        return Ok(product); 
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var result = await _productService.DeleteProductAsync(id);

        if (!result)
        {
            return NotFound($"No product found with given ID: {id}.");
        }

        return NoContent();
    }
}

