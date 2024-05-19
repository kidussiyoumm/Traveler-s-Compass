using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{
    public interface IAgentRepository
    {
       
            Task<Agent> CreateAgentAsync(Agent agent);
            Task<Agent> GetAgentIdAsync(int agentId);
            Task<Agent> GetAgentByNameAsync(string firstName,  string lastName);
            Task<List<Agent>> GetAllAgentsAsync();
            Task<Agent> UpdateAgentAsync(int agentId, Agent agent);
            Task<Agent> DeleteAgentAsync(int agentId);



        
    }
}
