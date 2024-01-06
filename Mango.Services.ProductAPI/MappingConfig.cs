using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.CouponAPI
{
    public class MappingConfig 
    {
        public static MapperConfiguration ResigterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();

            });
            return mappingConfig;
        }
    }
}
