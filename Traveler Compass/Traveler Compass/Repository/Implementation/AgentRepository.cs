﻿using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Models.DTO.Agent;
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
        public async Task<List<Agent>> GetAllAgentsAsync()
        {
            return await _dbContext.agents.ToListAsync();
        }
        //To create an Agent 
        public async Task<Agent> CreateAgentAsync(Agent agent)
        {
            if(agent == null) {
                throw new Exception("Please Try Again");
            }
            await _dbContext.agents.AddAsync(agent); //Agent table beign populated 
            await _dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return agent;
        }

        public async Task<Agent> GetAgentByNameAsync(string firstName, string lastName)
        {
            var selectedUser = await _dbContext.agents.FirstOrDefaultAsync(x => x.agentFristName == firstName && x.agentLastName == lastName);

            try
            {

                if(selectedUser == null)
                {
                    throw new Exception($"Invalid Entry of {selectedUser}");
                }
               
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return selectedUser;
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
            var exsitiingAgent = await _dbContext.agents.FindAsync(agentId);
            try
            {
                

                if(exsitiingAgent != null)
                {
                    exsitiingAgent.agentFristName = updatedAgent.agentFristName;
                    exsitiingAgent.agentLastName = updatedAgent.agentLastName;
                    exsitiingAgent.phoneNumber = updatedAgent.phoneNumber;
                    exsitiingAgent.email = updatedAgent.email;
                    exsitiingAgent.companyName = updatedAgent.companyName;
                    exsitiingAgent.description = updatedAgent.description;

                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("User not found");
                }
                
            }catch(Exception ex)
            {
                throw new Exception($"User doesn't exist, {ex.Message}");
            }

            return exsitiingAgent;
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

        public async Task<Agent> AuthenticateAgent(string email, string password)
        {
            try
            {
                var fetchUser = await _dbContext.agents.FirstOrDefaultAsync(x => x.email == email);

                if (fetchUser == null)
                {
                    throw new Exception($"Invalid {fetchUser}, try again ");
                }

                if (!matchingPassword(password, fetchUser.password, fetchUser.passwordKey))
                {
                    throw new Exception("Password doesn't match! Please try again");
                }


                return fetchUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private bool matchingPassword(string providedPassword, byte[] storedPasswordHash, byte[] storedPasswordKey)
        {
            using (var hmac = new HMACSHA512(storedPasswordKey))
            {

                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(providedPassword));  // converts the passwordText to a passwordHash

                for (int i = 0; i < computeHash.Length; i++)
                {

                    if (computeHash[i] != storedPasswordHash[i]) //takes the hash and compares to the password 
                    {
                        return false;
                    }
                }

                return true;
            }

        }
    }
}
