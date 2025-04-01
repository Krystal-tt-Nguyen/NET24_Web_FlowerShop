using AutoMapper;
using Webshop.Entities;
using Webshop.Shared.DTOs;

namespace Webshop.Profiles;

public class ProductCategoriesProfile : Profile
{
    public ProductCategoriesProfile()
    {
        CreateMap<ProductCategory, ProductCategoriesDto>();
    }
}
