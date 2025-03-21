using AutoMapper;
using Webshop.DTOs;
using Webshop.Entities;

namespace Webshop.Profiles;

public class OrderProfiles : Profile
{
    public OrderProfiles()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))      // mappa till customer
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)); // mappa till order items

        CreateMap<Order, CreateOrderDto>();

        CreateMap<CreateOrderDto, Order>();
    }
}
