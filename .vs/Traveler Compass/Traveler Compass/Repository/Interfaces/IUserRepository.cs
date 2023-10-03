using System.ComponentModel;
using Traveler_Compass.Models.Domain;

namespace Traveler_Compass.Repository.Interfaces
{//deals with the domain models
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);//This takes in user insert it in the database and return the created user
       // Task<User> GetUserAsync(string username);
        IEnumerable<User> GetAllUsers();
        Task<User> UpdateUserAsync(string userId, User user);
        Task<User> DeleteUserAsync(string userId);



    }
}
