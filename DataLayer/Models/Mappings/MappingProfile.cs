using AutoMapper;
using DataLayer.Models.Dtos;

namespace DataLayer.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
