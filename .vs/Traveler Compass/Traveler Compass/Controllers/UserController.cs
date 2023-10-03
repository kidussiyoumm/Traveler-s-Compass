using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Traveler_Compass.Data;
using Traveler_Compass.Models.Domain;
using Traveler_Compass.Repository.Interfaces;
using System.Net.Http;
using Traveler_Compass.Models.DTO;
using Traveler_Compass.Repository.Implementation;

namespace Traveler_Compass.Controllers
{
    [Produces("application/json")]  
    [Route("[controller]")]

    [ApiController]
    public class UserController : ControllerBase
    {
       //private readonly CompassDbContext dbContext; // we can use this feild inside the methods
       // (CompassDbContext dbContext) // DI using the compassDbCintext class in the controller, so that we get an instance of the dbcontext class
       //this.dbContext = dbContext;



       private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository) { //Any connection to the data base should be through the repository not directly to the Data base
           

            this.userRepository = userRepository;   
        }

        [HttpGet]

        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = userRepository.GetAllUsers();
            return Ok(users);
        }
    

    [HttpPost]
        [Route("post")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
        {

            //Map Dto to domain model
            //So that the clients can't have access to certain field such as userId
            var user = new User //this the user class
            {
                userId = userDto.userId,
                firstName = userDto.firstName,
                lastName = userDto.lastName,
                email = userDto.email,
                password = userDto.password,
                phoneNumber = userDto.phoneNumber,
                gender = userDto.gender


            };

            await userRepository.CreateUserAsync(user); //take in the user and pass it to the UserRepo





            //Map back from model to Dto
            var response = new UserDto
            {
                userId = user.userId,
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                password = user.password,
                phoneNumber = user.phoneNumber,
                gender = user.gender

            };
            return Ok(response); // not to use "user" directly its bad practice 
        }


        [HttpPut("{string}")]
        public async Task<IActionResult> UpdateUser(string userid, [FromBody] User user)
        {
            if(userid != user.userId)
            {
                return BadRequest("ID mismatch between URL and user data.");

            }
            try
            {
                userRepository.UpdateUserAsync(userid, user);
                return Ok();
            }
            catch(Exception ex) 
            {

                return StatusCode(500, "Internal server error");

            }

        }

        [HttpDelete]
       // [Route("{string}")]
        public async Task<IActionResult> DeleteUser(string userId)

        {
            var seletctedUser = await userRepository.DeleteUserAsync(userId);

            if(seletctedUser != null)
            {
                return NotFound();
            }

            return Ok();    


        }


    }
}
