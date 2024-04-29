using AutoMapper;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.UserDto;

namespace Traveler_Compass.AutoMapper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<User, UserDTO>(); 
            CreateMap<UserDTO, User>();
            CreateMap<CreateUserDto, User>();
            CreateMap<User, CreateUserDto>();
        }
    }
}
