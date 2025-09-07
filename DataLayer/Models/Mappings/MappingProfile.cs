using AutoMapper;
using DataLayer.Models.Dtos;

namespace DataLayer.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ForMember(dest => dest.Status,
                                                   opt => opt.MapFrom(src => src.Status.ToString()))
                                        .ReverseMap().ForMember(dest => dest.Status,
                                                                opt => opt.MapFrom(src => Enum.Parse<OrderStatus>(src.Status))
                                                                );
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
