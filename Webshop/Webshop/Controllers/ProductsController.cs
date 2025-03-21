using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Webshop.DTOs;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository
            ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _productRepository.GetProductsAsync();
        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductById(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);

        if (product is null) 
        {
            return NotFound($"No product found with given ID: {id}, please try again.");
        }

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<ProductDto>> GetProductByName(string name)
    {
        var product = await _productRepository.GetProductByNameAsync(name);

        if (product is null)
        {
            return NotFound($"No product found with given productname: {name}, please try again.");
        }

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductDto>> CreateProduct(CreateProductDto newProduct) 
    {
        if (newProduct is null)
        {
            return BadRequest("Product data cannot be null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingProduct = await _productRepository.GetProductByProductNumberAsync(newProduct.ProductNumber);

            if (existingProduct != null)
            {
                return Conflict("Cannot add product: An identical product is already registered.");
            }

            var product = _mapper.Map<Product>(newProduct);

            await _productRepository.CreateProductAsync(product);
            await _productRepository.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateProductDto>> UpdateProduct(int id, UpdateProductDto updatedProduct)
    {
        if (updatedProduct is null)
        {
            return BadRequest("Product data cannot be null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);

            if (existingProduct is null)
            {
                return NotFound($"No product found with given ID: {id}, please try again.");
            }

            // _mapper.Map(source, destination);
            _mapper.Map(updatedProduct, existingProduct);

            _productRepository.UpdateProduct(existingProduct);
            await _productRepository.SaveChangesAsync();

            return Ok(_mapper.Map<ProductDto>(existingProduct));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(500, "An error occurred while processing your request.");
        }        
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var existingProduct = await _productRepository.GetProductByIdAsync(id);

        if (existingProduct is null)
        {
            return NotFound($"No product found with given ID: {id}, please try again.");
        }

        _productRepository.DeleteProduct(existingProduct);
        await _productRepository.SaveChangesAsync();

        return NoContent();
    }
}

