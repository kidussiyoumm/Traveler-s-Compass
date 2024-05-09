using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler_Compass.Repository.Implementation;
using Traveler_Compass.Repository.Interfaces;
using FakeItEasy;
using Traveler_Compass.Models.DTO.UserDto;
using Xunit;

namespace TravelCompass.XUnit.Test.Controllers
{
    //we are using a Mock Test cause we don't want our database or any other thing
    //to preform an excusion rather we are faking it using fakeitEasy
    public class UserControllerTests
    {
        //private readonly UserRepository _userRepository;
        private readonly IUserRepository _IuserRepository;
        public UserControllerTests() 
        { 
        //A(From FakeitEASY) provides methods for generating fake objects
        _IuserRepository = A.Fake<IUserRepository>();
        
        }

        [Fact]
        public void UserController_GetUsers_ReturnOK()
        {
            //Arrangr
            var users = A.Fake<ICollection<UserDTO>>();
            var usersList = A.Fake<IList<UserDTO>>();


            //ACT

            
            //Assert


        }

    }
}
