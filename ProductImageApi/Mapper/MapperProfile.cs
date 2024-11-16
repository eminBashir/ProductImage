using AutoMapper;
using ProductImageApi.Controllers;
using ProductImageApi.Data.Entity;
using ProductImageApi.Model.DTO;

namespace ProductImageApi.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product,ProductGetDTO>();
        }
    }
}
