﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;

namespace Traveler_Compass.Repository.Implementation
{          
    public class UserRepository : IUserRepository
    {
        //we will never deal with the DTOs inside the repositiories, thats for the controller
        /// <summary>
        /// SaveChanges() method in Entity Framework Core is not inherently
        /// asynchronous, so there's no need to await it. It operates synchronously 
        /// and returns the number of state entries written to the database.
        /// </summary>

        private readonly CompassDbContext _dbContext; 
        public UserRepository(CompassDbContext dbContext) //DI from compassDbContext class
        { 
            this._dbContext = dbContext;
        }

    //To get a user from DB
    [Authorize]
    [HttpGet]
    public async Task<List<User>>GetAllUsersAsync()
    {
        return await _dbContext.users.ToListAsync();
    }

     //To create a user   
    public async Task<User> CreateUserAsync(User user) //we will call this in the controller class 
        {
            await _dbContext.users.AddAsync(user); //User table beign populated 
            await _dbContext.SaveChangesAsync(); //EFCORE saving it to the db

            return user;
        }

        //To update current user
        [Authorize]
        public async Task<User> UpdateUserAsync(int userId, User updatedUser)
        {
          
          var existingUser = await _dbContext.users.FindAsync(userId);

            try
            {
                if (existingUser != null)
                {
                  
                    existingUser.firstName = updatedUser.firstName;
                    existingUser.lastName = updatedUser.lastName;
                    existingUser.email = updatedUser.email;
                    existingUser.phoneNumber = updatedUser.phoneNumber;
                    existingUser.password = updatedUser.password;
                    existingUser.gender = updatedUser.gender;

                   
                    await _dbContext.SaveChangesAsync();
 
                }
                else
                {
                    throw new Exception("user not found in Database");
                }

                return existingUser;

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            

        }

        //To delete a user from the database with matching userId passed
        public async Task<User> DeleteUserAsync(int userId)

        { 
            
            var selectedUser = await _dbContext.users.FindAsync(userId);
            try
            {
                if (selectedUser == null)
                {
                    throw new Exception($"Agent with ID {userId} not found.");

                }

                _dbContext.users.Remove(selectedUser);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
   
           
            return selectedUser;
        

        }


        //To fetch a user from the database with matching username passed
        public async Task<User> GetUserByNameAsync(string firstName, string lastName)
        {
            var selectedUser = await _dbContext.users.FirstOrDefaultAsync(x => x.firstName == firstName && x.lastName == lastName);
            try
            {
                if (selectedUser == null)
                {
                    throw new Exception($"Uswer with ID {selectedUser} not found.");

                }
  
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }

           

            return selectedUser;

        }

        //To Get a user from the database with matching userId passed
        public async Task<User> GetUserByIdAsync(int userId)
        {
            var selectedUser = await _dbContext.users.FindAsync(userId);
            try
            {
                if (selectedUser == null)
                {
                    throw new Exception($" user with ID {userId} not found.");

                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }


            return selectedUser;
        }


       public async Task<User> GetUserByEmailAsync(string email)
        {

            try
            {
                var fetchEmail = await _dbContext.users.FirstOrDefaultAsync(x => x.email == email);

                if(fetchEmail == null) { 
               
                    throw new Exception($"{fetchEmail} was a null entry");
                }

                return fetchEmail;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception($"No user found with email {email}"); ;
            }
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            try
            {
                var fetchUser = await _dbContext.users.FirstOrDefaultAsync(x => x.email == email);
                
                if (fetchUser == null)
                {
                    throw new Exception($"Invalid {fetchUser}, try again ");
                }
                
                if(!matchingPassword(password, fetchUser.password , fetchUser.passwordKey))
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
            using ( var hmac = new HMACSHA512(storedPasswordKey))
            {
                
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(providedPassword));  // converts the passwordText to a passwordHash

                for(int i = 0; i < computeHash.Length; i++) {

                   if(computeHash[i] != storedPasswordHash[i]) //takes the hash and compares to the password 
                    {
                        return false;
                    }                
                }

                return true;
            }

        }
       


      
    }
}
