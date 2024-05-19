using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly CompassDbContext _dbContext;
        public RegisterRepository(CompassDbContext _dbContext)
        {        
          this._dbContext = _dbContext;
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

        public async Task<object> RegisterUserAsync(User users, string password, bool isAgent)
        {
            try
            {
                // Validate email format
                if (!await IsValidEmailAsync(users.email))
                {
                    throw new ArgumentException("Invalid email format");
                }
                if (await UserAlreadyExists(users.email))
                {
                    throw new ArgumentException($"User {users.email} alreadt exists, please try again");
                }


                if (isAgent)
                {
                   var agent = new Agent
                    {
                        agentFristName = users.firstName,
                        agentLastName = users.lastName,
                        password = users.password,
                        email = users.email,
                        phoneNumber = users.phoneNumber,
                        agentGender = users.gender

                    };
                    return await RegisterAgentAsync(agent, password);

                }

                byte[] passwordHash; //Generates a random key when it is initialized 
                byte[] passwordKey;
            
                using (var hmac = new HMACSHA512())//Hmac stands for hash-base message authentication code
                {
                    passwordKey = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));//This methods accepts a byte array       
                                                                                                  //we get it from a string then encoding it        
                }

                User user = new User(); //Creating a user to add it into database 
                user.email = users.email;
                user.password = passwordHash;
                user.passwordKey = passwordKey;
                user.phoneNumber = users.phoneNumber;
                user.firstName = users.firstName;
                user.lastName = users.lastName; 
                user.gender = users.gender;


                await _dbContext.users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }
        }

        public async Task<Agent> RegisterAgentAsync(Agent agents, string password)
        {
            try
            {
                // Validate email format
                if (!await IsValidEmailAsync(agents.email))
                {
                    throw new ArgumentException("Invalid email format");
                }
                if(await UserAlreadyExists(agents.email))
                {
                    throw new Exception($"Agent {agents.email} already exists, please enter a correct email again!");
                }

                byte[] passwordHash; //Generates a random key when it is initialized 
                byte[] passwordKey;

                using (var hmac = new HMACSHA512())//Hmac stands for hash-base message authentication code
                {
                    passwordKey = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));//This methods accepts a byte array       
                                                                                                  //we get it from a string then encoding it        
                }

                Agent agent = new Agent();  
                agent.email = agents.email;
                agent.agentFristName = agents.agentFristName;
                agent.agentLastName = agents.agentLastName; 
                agent.phoneNumber = agents.phoneNumber;
                agent.passwordKey = passwordKey;
                agent.password = passwordHash;
                agent.agentGender = agents.agentGender;
                agent.companyName = agents.companyName;
                agent.description = agents.description;

                await _dbContext.agents.AddAsync(agent);
                await _dbContext.SaveChangesAsync();

                return agent;
 
            }
             catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

            }      
          
        }
        

        public async Task<bool> UserAlreadyExists(string email)
        {
            return await _dbContext.users.AnyAsync(x => x.email == email);
        }
    }

}
