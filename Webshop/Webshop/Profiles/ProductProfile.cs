using AutoMapper;
using Webshop.Shared.DTOs;
using Webshop.Entities;

namespace Webshop.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {        
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ProductCategoryName, opt => opt.MapFrom(src => src.ProductCategory.CategoryName));

        CreateMap<CreateProductDto, Product> ();
        CreateMap<UpdateProductDto, Product> ();
    }
}
