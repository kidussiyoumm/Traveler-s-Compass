using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{
    public class RegisterRepository : IRegisterRepository
    {

        private readonly IUserRepository _userRepository;
        private readonly IAgentRepository _agentRepository;
    

        public RegisterRepository(IUserRepository _userRepository,
                                  IAgentRepository _agentRepository
                                 )
        {
            this._userRepository = _userRepository;
            this._agentRepository = _agentRepository;
        
        }

        public async Task<bool> IsValidEmailAsync(string email)
        {
            // Define a regular expression pattern for email validation
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object with the email pattern
            Regex regex = new Regex(emailPattern);

            // Perform the validation synchronously
            return await Task.FromResult(regex.IsMatch(email));
        }

        public async Task<object> RegisterUserAsync(User users, bool isAgent)
        {
            try
            {
                // Validate email format
                if (!await IsValidEmailAsync(users.email))
                {
                    throw new ArgumentException("Invalid email format");
                }

                // Check if email is already registered
                //if (await _userRepository.GetUserByEmailAsync(users.email) != null)
                //{
                //    throw new ArgumentException("Email already exists");
                //}

                if (isAgent)
                {
                   var agent = new Agent
                   //var registeredAgent = await RegisterAgentAsync(new Agent
                    {
                        agentFristName = users.firstName,
                        agentLastName = users.lastName,
                        email = users.email,
                        phoneNumber = users.phoneNumber

                    };
                    return await RegisterAgentAsync(agent);

                }
                await _userRepository.CreateUserAsync(users);
            

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
        }

        public async Task<Agent> RegisterAgentAsync(Agent agent)
        {
            try
            {
                // Validate email format
                if (!await IsValidEmailAsync(agent.email))
                {
                    throw new ArgumentException("Invalid email format");
                }

                // Check if email is already registered
                //if (await _userRepository.GetUserByEmailAsync(agent.email) != null)
                //{
                //    throw new ArgumentException("Email already exists");
                //}

                await _agentRepository.CreateAgentAsync(agent);
             

               
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            } 
            
            return agent;
        }



    }


}
