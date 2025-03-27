using AutoMapper;
using Webshop.Shared.DTOs;
using Webshop.Entities;
using Webshop.Interfaces;

namespace Webshop.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository 
            ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper 
            ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        
        if (product is null)
        {
            return null;
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> GetProductByNameAsync(string name)
    {
        var product = await _productRepository.GetProductByNameAsync(name);

        if (product is null)
        {
            return null;
        }

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto newProduct)
    {
        var existingProduct = await _productRepository.GetProductByProductNumberAsync(newProduct.ProductNumber);
        
        if (existingProduct != null)
        {
            return null; // Produkten finns redan i sortiment
        }

        var product = _mapper.Map<Product>(newProduct);

        await _productRepository.CreateProductAsync(product);
        await _productRepository.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updatedProduct)
    {
        var existingProduct = await _productRepository.GetProductByIdAsync(id);

        if (existingProduct is null)
        {
            return null;
        }

        _mapper.Map(updatedProduct, existingProduct);

        _productRepository.UpdateProduct(existingProduct);

        await _productRepository.SaveChangesAsync();

        return _mapper.Map<ProductDto>(existingProduct);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var existingProduct = await _productRepository.GetProductByIdAsync(id);

        if (existingProduct is null)
        {
            return false;
        }

        _productRepository.DeleteProduct(existingProduct);
        await _productRepository.SaveChangesAsync();

        return true;
    }
}

