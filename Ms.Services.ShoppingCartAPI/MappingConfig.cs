using AutoMapper;
using Ms.Services.ShoppingCartAPI.Models;
using Ms.Services.ShoppingCartAPI.Models.Dto;

namespace Ms.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetail, CartDetailsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
