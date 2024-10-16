using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LibraryBE.Controllers;
using LibraryBE.DTO;
using LibraryBE.Interfaces;
using LibraryBE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBE.Tests.Controller
{
    public class UserControllerTest
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserControllerTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void UserController_GetAllUsers_ReturnOK()
        {
            //Arrange 
            var users = A.Fake<ICollection<UserDto>>();
            var userList = A.Fake<List<UserDto>>();
            A.CallTo(() => _mapper.Map<List<UserDto>>(users)).Returns(userList);
            var controller = new UserController(_userRepository, _mapper);
            //Act
            var result = controller.GetAllUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [Fact]
        public void UserController_GetUser_ReturnOK()
        {
            // Arrange
            int userId = 1;
            var userDto = A.Fake<UserDto>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _mapper.Map<UserDto>(_userRepository.GetUser(userId))).Returns(userDto);
            var controller = new UserController(_userRepository, _mapper);

            // Act
            var result = controller.GetUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }


        [Fact]
        public void UserController_GetUser_ReturnNotFound()
        {
            // Arrange
            int userId = 1;
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(false);
            var controller = new UserController(_userRepository, _mapper);

            // Act
            var result = controller.GetUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
        }

        [Fact]
        public void UserController_GetUser_ReturnBadRequest()
        {
            // Arrange
            int userId = 1;
            var userDto = A.Fake<UserDto>();
            A.CallTo(() => _userRepository.UserExists(userId)).Returns(true);
            A.CallTo(() => _mapper.Map<UserDto>(_userRepository.GetUser(userId))).Returns(userDto);

            var controller = new UserController(_userRepository, _mapper);
            controller.ModelState.AddModelError("test", "test error"); // Simulate invalid ModelState

            // Act
            var result = controller.GetUser(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
        }

        [Fact]
        public async Task UserController_Login_ReturnsOk()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123" };
            var user = A.Fake<User>();
            var userDto = A.Fake<UserDto>();

            A.CallTo(() => _userRepository.Login(loginDto.Email, loginDto.Password)).Returns(user);
            A.CallTo(() => _mapper.Map<UserDto>(user)).Returns(userDto);

            var controller = new UserController(_userRepository, _mapper);

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [Fact]
        public async Task UserController_Login_MissingEmailOrPassword_ReturnsBadRequest()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "", Password = "" }; // Empty email and password
            var controller = new UserController(_userRepository, _mapper);

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
        }

        [Fact]
        public async Task UserController_Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "WrongPassword" };

            A.CallTo(() => _userRepository.Login(loginDto.Email, loginDto.Password)).Returns(null); // No user found

            var controller = new UserController(_userRepository, _mapper);

            // Act
            var result = await controller.Login(loginDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.UnauthorizedResult));
        }


    }
}
