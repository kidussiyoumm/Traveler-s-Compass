using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{
    public class AgentRepository : IAgentRepository
    {
        private readonly CompassDbContext _dbContext;
        public AgentRepository(CompassDbContext dbContext){ 
            this._dbContext = dbContext;
        }
        //To get Agent from Database returns a list of Agnets
        public async Task<List<Agent>> GetAllAgents()
        {
            return await _dbContext.agents.ToListAsync();
        }
        //To create an Agent 
        public async Task<Agent> CreateAgentAsync(Agent agent)
        {
            await _dbContext.agents.AddAsync(agent); //Agent table beign populated 
            await _dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return agent;
        }
        //To delete a agent from the database with matching agentId passed
        public async Task<Agent> DeleteAgentAsync(int agentId)
        {

            var selectedUser = await _dbContext.agents.FindAsync(agentId);
            try
            {
                if (selectedUser == null)
                {
                    throw new Exception($"Agent with ID {agentId} not found.");

                }

                _dbContext.agents.Remove(selectedUser);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }

            return selectedUser;
        }


        public async Task<Agent> UpdateAgentAsync(int agentId, Agent updatedAgent)
        {
            try
            {
                var exsitiingAgent = await _dbContext.agents.FindAsync(agentId);

                if(exsitiingAgent != null)
                {
                    exsitiingAgent.agentFristName = updatedAgent.agentFristName;
                    exsitiingAgent.agentLastName = updatedAgent.agentLastName;
                    exsitiingAgent.phoneNumber = updatedAgent.phoneNumber;
                    exsitiingAgent.email = updatedAgent.email;
                    exsitiingAgent.companyName = updatedAgent.companyName;
                    exsitiingAgent.description = updatedAgent.description;
                }
                else
                {
                    throw new Exception("User not found");
                }
                return updatedAgent;
            }catch(Exception ex)
            {
                throw new Exception($"User doesn't exist, {ex.Message}");
            }

        }

        //Get Agent by passing an Id to fetch from the database
        public async Task<Agent> GetAgentIdAsync(int agentId)
        {
            var selectedId = await _dbContext.agents.FindAsync(agentId);
            try
            {
                if(selectedId == null)
                {
                    throw new Exception($"Aginet with {selectedId} is not found");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return selectedId;
        }
    }
}
