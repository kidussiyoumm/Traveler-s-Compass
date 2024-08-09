using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler_Compass.Repository.Interfaces;
using FakeItEasy;
using Traveler_Compass.Models.DTO.UserDto;
using Traveler_Compass.Controllers;
using Traveler_Compass.Models.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;


namespace TravelCompass.Tests.Controllers.Tests
{
    public class UserControllerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserController _controller;

        public UserControllerTests() {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
            _controller = new UserController(_userRepository, _mapper);
        }


        [Fact]
        public async Task GetUserByAsync_ReturnsUserId_WhenUserExist()
        {
            //Arrange
            int userId = 1;
            var user = new User { userId = userId }; // Setup a user with the same ID
            var userDTO = new UserDTO(); // Setup the DTO
          

            // Setup repository to return the fake User
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(Task.FromResult(user));
            // Setup mapper to map User to UserDTO
            A.CallTo(() => _mapper.Map<UserDTO>(user)).Returns(userDTO);


            // Act
            var result = await _controller.GetUserByIdAsync(userId);


            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUserDTO = Assert.IsType<UserDTO>(okResult.Value);
            Assert.Equal(userDTO, returnedUserDTO);
        }

        [Fact]
        public async Task GetUserByAsync_ReturnsUserId_WhenDoesNotExist()
        {
            //Arrange
            int userId = 1;

            // Setup repository to return the fake User
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(Task.FromResult<User>(null));
            //This means that when GetUserByIdAsync is called with userId, it should return null, simulating that no user was found.


            // Act
            var result = await _controller.GetUserByIdAsync(userId); //passing the userId and capturing the result.


            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);  //This checks that the result is of type NotFoundResult.  
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsBadRequest_WhenIDMismatch()
        {


            //Arrange 
            int userId = 1;
            var user = new User { userId = userId + 1 }; // User with a different ID
            A.CallTo(() => _userRepository.GetUserByIdAsync(userId)).Returns(Task.FromResult(user));

            //Act
            var result = await _controller.GetUserByIdAsync(userId);

            //Assert
            var actionResult = Assert.IsType<ActionResult<UserDTO>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result); //checks that the result of the controller action is of type BadRequestObjectResult
            Assert.Equal("ID mismatch.", badRequestResult.Value);//further check that the message inside the BadRequestObjectResult is exactly "ID mismatch."





        }


    }




}