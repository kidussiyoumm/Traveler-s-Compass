using AutoMapper;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.Agent;
using Traveler_Compass.Models.DTO.AgentDto;
using Traveler_Compass.Models.DTO.ItineraryDto;
using Traveler_Compass.Models.DTO.LoginDto;
using Traveler_Compass.Models.DTO.PacakgeDto;
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

            CreateMap<Agent, AgentDTO>();
            CreateMap<AgentDTO, Agent>();

            CreateMap<CreateAgentDTO, Agent>();
            CreateMap<Agent, CreateAgentDTO>();

            CreateMap<Itinerary, ItineraryDTO>();
            CreateMap<ItineraryDTO, Itinerary>();

            CreateMap<CreateItineraryDTO, Itinerary>();
            CreateMap<Itinerary, CreateItineraryDTO>();

            CreateMap<Package, PackageDTO>();
            CreateMap<PackageDTO, Package>();   

            CreateMap<Package, CreatePackageDTO>();
            CreateMap<CreatePackageDTO, Package>();

            CreateMap<User, LoginReqDTO>();
            CreateMap<LoginReqDTO, User>();

            //CreateMap<RegisterDTO, User>(); // Map RegistrationDto to User
            //CreateMap<User, RegisterDTO>(); // Map User to RegistrationDto

            //CreateMap<RoleDTO, Role>(); // Map RoleDto to Role
            //CreateMap<Role, RoleDTO>(); // Map Role to RoleDto
        }
    }
}
