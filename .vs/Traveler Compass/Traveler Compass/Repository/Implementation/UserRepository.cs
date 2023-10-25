using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        //we will never deal with the DTOs inside the repositiories, thats for the controller

        private readonly CompassDbContext dbContext; //
        public UserRepository(CompassDbContext dbContext) //DI from compassDbContext class
        { 
            this.dbContext = dbContext;
        }

    //To get a user from DB
    public IEnumerable<User> GetAllUsers()
    {
        return dbContext.users.ToList();
    }

    //To create a user
    public async Task<User> CreateUserAsync(User user) //we will call this in the controller class 
        {
            await dbContext.users.AddAsync(user); //User table beign populated 
            await dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return user;
        }

        //To update current user
      
        public async Task<User> UpdateUserAsync(int userId, User user)
        {
            var updateUser = dbContext.users.FindAsync(userId);
          if (updateUser != null)
            {
               // user.userId = user.userId;
                user.firstName = user.firstName;
                user.lastName = user.lastName;
                user.email = user.email;
                user.phoneNumber = user.phoneNumber;
                user.password = user.password;
                user.gender = user.gender;

               await dbContext.users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }

             return user;

        }

        [HttpDelete]
        public async Task<User> DeleteUserAsync(int userId)

        { 
            
            var selectedUser = await dbContext.users.FindAsync(userId);
            if (selectedUser == null)
            {
                return null;


            }

            dbContext.users.Remove(selectedUser);
            await dbContext.SaveChangesAsync();
            return selectedUser;




           

        }



        //public Task<User> GetUserAsync(string username)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
