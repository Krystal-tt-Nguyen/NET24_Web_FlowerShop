using AutoMapper;
using Webshop.Shared.DTOs;
using Webshop.Entities;

namespace Webshop.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();        
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();        
    }
}

/* SYNTAX
    CreateMap<SourceType, DestinationType>()
        .ForMember(dest => dest.DestinationProperty, opt => opt.MapFrom(src => src.RelatedEntity.Property)); 
*/