using AutoMapper;
using Webshop.DTOs;
using Webshop.Entities;

namespace Webshop.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)) // Hämta produktnamn från Product
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Product.Price)); // Beräkna TotalPrice

        CreateMap<OrderItemDto, OrderItem>();
        
        CreateMap<CreateOrderItemDto, OrderItem>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
    }
}

