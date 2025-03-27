using AutoMapper;
using Webshop.Shared.DTOs;
using Webshop.Entities;

namespace Webshop.Profiles;

public class OrderProfiles : Profile
{
    public OrderProfiles()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))    
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<Order, CreateOrderDto>();

        CreateMap<CreateOrderDto, Order>();
    }
}
