using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{
    public interface IRegisterRepository
    {
        Task<Object>RegisterUserAsync(User users, bool isAgent);
        Task<Agent> RegisterAgentAsync(Agent agent);
        
    }
}
