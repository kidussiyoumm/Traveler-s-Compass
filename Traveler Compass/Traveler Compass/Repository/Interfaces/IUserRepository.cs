using System.ComponentModel;
using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{//deals with the domain models
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);//This takes in user insert it in the database and return the created user
        Task<User> GetUserByNameAsync(string userFristName, string userLastName);
        Task<List<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(int userId, User user);
        Task<User> DeleteUserAsync(int userId);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> AuthenticateUser(string email,string password);
        
       
      



    }
}
