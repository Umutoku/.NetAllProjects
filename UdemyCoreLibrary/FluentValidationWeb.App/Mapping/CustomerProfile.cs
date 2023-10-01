using AutoMapper;
using FluentValidationWeb.App.DTOs;
using FluentValidationWeb.App.Models;

namespace FluentValidationWeb.App.Mapping
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.Isim, opt => opt.MapFrom(x => x.Name));
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.EMail, opt => opt.MapFrom(x => x.Posta));
        }
    }
}
