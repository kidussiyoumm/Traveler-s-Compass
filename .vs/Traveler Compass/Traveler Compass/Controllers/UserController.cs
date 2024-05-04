﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;
using System.Net.Http;
using Traveler_Compass.Repository.Implementation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using Traveler_Compass.Models.DTO.UserDto;

namespace Traveler_Compass.Controllers
{
    [Produces("application/json")]  
    [Route("[controller]")]

    [ApiController]
    public class UserController : Controller
    {
       //private readonly CompassDbContext dbContext; // we can use this feild inside the methods
       //(CompassDbContext dbContext) //DI using the compassDbContext class in the controller, so that we get an instance of the dbcontext class
       //this.dbContext = dbContext;



        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper) { //Any connection to the data base should be through the repository not directly to the Data base
           

            this._userRepository = userRepository;   
            this._mapper = mapper;
        }

       
        [HttpGet]
        //Fetchs all users
        public async Task<ActionResult<List<UserDTO>>>GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            //we want to select and return the DTO instead of the USER class
            var userDTOs = users.Select(user => _mapper.Map<UserDTO>(user)).ToList();
            return Ok(userDTOs);
        }

        [HttpGet("{userId}")]
        //Fetchs user using a userId
        public async Task<ActionResult<UserDTO>> GetUserByID(int userId)
        {
            var fetchedData = await _userRepository.GetUserByIdAsync(userId);

            var mapData = _mapper.Map<UserDTO>(fetchedData);
            try
            {
                if(fetchedData.userId != userId || fetchedData == null)
                {
                    return BadRequest("ID mismatch.");
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return Ok(mapData);
        }


        [HttpGet("user")]

        public async Task<IActionResult> GetUserByName(string firstName, string lastName)
        { 
            
            try
            {
               
                if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    return BadRequest("First Name and Last Name is Required");
                }
                //Retrieve the user by first and last name from the User Repo
                var userName = await _userRepository.GetUserAsync(firstName, lastName);


                if(userName != null)
                {
                    //Map the user Entity to the UserDTO
                    var mapData = _mapper.Map<UserDTO>(userName);
                    return Ok(mapData);
                }
                else
                {
                    return NotFound();
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);

            }

                
        } 



        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {
            try
            {
                //Converts the clients data from DTO to our representation of User
                var client = _mapper.Map<User>(userDto);

                var repo = await _userRepository.CreateUserAsync(client);

                var respone = _mapper.Map<UserDTO>(repo);

                return Ok(respone);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"There was an error found{ex.Message}");

            }
          
        }


        //this method will take a userId passed by the user to fetch the data from the repository 
        //The repository class will then update the user
        [HttpPut]
        [Route("{userId}")] 
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDTO userDto){
           var fetchedData = await _userRepository.GetUserByIdAsync(userId);
            
            try
            {
                if (fetchedData.userId != userId)
                {
                    return BadRequest("ID mismatch between URL and user data.");

                }
                if(fetchedData == null)
                {
                    return NotFound();
                }

                // Map the updated user DTO to a user entity
                var clentRequest = _mapper.Map<User>(userDto);

                // Update the user in the repository
                await _userRepository.UpdateUserAsync(userId, clentRequest);

                // Map the updated user entity back to a DTO
                var response = _mapper.Map<UserDTO>(clentRequest);
              
                return Ok(response);

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
               return StatusCode(500);
            }
        
        }

        //This Method is used to Fetch user Id from User Repository and delete it from the database
        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)

        { 
            
            //no need to map the deleted user to a DTO since the user
            //is no longer available after deletion
            try
            {   //Fetchs the same userId
               var selectedUser = await _userRepository.GetUserByIdAsync(userId);
                if (selectedUser.userId == userId)
                {   //Deletes user from User Repo
                    await _userRepository.DeleteUserAsync(userId);
                }
 
                
                return Ok(); 

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }

            


        }

       


    }
}
