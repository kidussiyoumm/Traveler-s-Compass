using Microsoft.AspNetCore.Http;
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
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Traveler_Compass.Helper;

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
        [Route("api/Users/GetAllUserAsync")]
        //Fetchs all users
        public async Task<ActionResult<List<UserDTO>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                //we want to select and return the DTO instead of the USER class back to client 
                var userDTO = users.Select(user => _mapper.Map<UserDTO>(user)).ToList();

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
           
        }

        [HttpGet("api/Users/{userId}/GetByIdAsync")]
        //Fetchs user using a userId
        public async Task<ActionResult<UserDTO>> GetUserByIdAsync(int userId)
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


        [HttpGet]
        [Route("api/Users/{firstName}/{lastName}/GetUserByNameAsync")]

        public async Task<IActionResult> GetUserByNameAsync(string firstName, string lastName)
        { 
            
            try
            {
               
                if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    return BadRequest("First Name and Last Name is Required");
                }
                //Retrieve the user by first and last name from the User Repo
                var userName = await _userRepository.GetUserByNameAsync(firstName, lastName);


                if(userName != null)
                {
                    //Map the user Entity to the UserDTO
                    var mapData = _mapper.Map<UserDTO>(userName);
                    return Ok(mapData);
                }
                else
                {
                    return NotFound($"{userName} not Found");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500);

            }

                
        } 



        [HttpPost]
        [Route("api/Users/CreateUserAsync")]
        [Authorize(Roles = "User")] // Secure this action for users with the "Admin" role
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto userDto)
        {
            try
            {
                //Converts the clients data from DTO to our representation of User
                var client = _mapper.Map<User>(userDto);

                var user = await _userRepository.CreateUserAsync(client);

                var respone = _mapper.Map<UserDTO>(user);

                return Ok(respone);

              //  var token = JwtTokenUtility.GenerateToken(user.userId.ToString(), user.firstName, user.); // Include user role

              //  return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"There was an error found{ex.Message}");

            }
          
        }


        //this method will take a userId passed by the user to fetch the data from the repository 
        //The repository class will then update the user
        [HttpPut]
        [Route("api/Users/{userId}/updateUserAsync")] 
        public async Task<IActionResult> UpdateUserAsync(int userId, [FromBody] UserDTO userDto){
           var fetchedId = await _userRepository.GetUserByIdAsync(userId);
            
            try
            {
                if (fetchedId.userId != userId)
                {
                    return BadRequest("ID mismatch between URL and user data.");

                }
                if(fetchedId == null)
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
        [Route("api/Users/{userId}/DeleteUserAsync")]
        public async Task<IActionResult> DeleteUserAsync(int userId)

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
