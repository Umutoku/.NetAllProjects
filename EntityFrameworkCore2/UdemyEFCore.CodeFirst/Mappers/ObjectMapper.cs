using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyEFCore.CodeFirst.DAL;
using UdemyEFCore.CodeFirst.DTOs;

namespace UdemyEFCore.CodeFirst.Mappers
{
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });
            return config.CreateMapper();
        });
        public static IMapper Mapper => lazy.Value;
    }


    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }


}
