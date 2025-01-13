using AutoMapper;
using ECommerceApi.DTOs;
using ECommerceApi.Models;

namespace ECommerceApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDTO>();
        }
    }
}