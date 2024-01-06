using AutoMapper;
using Mango.Services.CartAPI.Models;
using Mango.Services.CartAPI.Models.Dto;

namespace Mango.Services.CartAPI
{
    public class MappingConfig 
    {
        public static MapperConfiguration ResigterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
