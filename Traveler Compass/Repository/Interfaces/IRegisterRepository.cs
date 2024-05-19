using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{
    public interface IRegisterRepository
    {
        Task<Object>RegisterUserAsync(User users, string password, bool isAgent);
        Task<Agent> RegisterAgentAsync(Agent agent, string password);
        Task <bool> UserAlreadyExists(string email);
    }
}
