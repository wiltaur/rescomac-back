using Microsoft.AspNetCore.Mvc;
using Moq;
using rescomac_back.business.Interface;
using rescomac_back.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace rescomac_back.Test.ControllerTest
{
    public class LoginControllerTest
    {
        private LoginController _currentController;
        private readonly Mock<ILoginService> _loginService = new();


        public LoginControllerTest()
        {
            _currentController = new LoginController(_loginService.Object);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 400)]
        public async Task ValidarUsuarioTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _loginService
                        .Setup(t => t.ValidarUsuario(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _loginService
                        .Setup(t => t.ValidarUsuario(It.IsAny<string>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(false));
                    break;
                case 3:
                    _loginService
                        .Setup(t => t.ValidarUsuario(It.IsAny<string>(), It.IsAny<string>()))
                        .Throws(new Exception("Error"));
                    break;
            }
            //Act
            IActionResult response = await _currentController.ValidarUsuario(It.IsAny<string>(), It.IsAny<string>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }
    }
}