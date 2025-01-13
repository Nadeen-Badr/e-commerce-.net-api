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
             CreateMap<Cart, CartResponseDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<Order, OrderResponseDTO>()
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            // Map OrderItem to OrderItemResponseDTO
            CreateMap<OrderItem, OrderItemResponseDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price));
        }
    }
}