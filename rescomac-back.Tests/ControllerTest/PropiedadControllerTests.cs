using Microsoft.AspNetCore.Mvc;
using Moq;
using rescomac_back.business.Interface;
using rescomac_back.Controllers;
using rescomac_back.repository.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace rescomac_back.Tests.ControllerTest
{
    public class PropiedadControllerTests
    {
        private PropietarioController _currentController;
        private readonly Mock<IPropietarioService> _propiedadService = new();


        public PropiedadControllerTests()
        {
            _currentController = new PropietarioController(_propiedadService.Object);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        public async Task CreatePropiedadTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _propiedadService
                        .Setup(t => t.AddPropiedad(It.IsAny<PropiedadRequest>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _propiedadService
                        .Setup(t => t.AddPropiedad(It.IsAny<PropiedadRequest>()))
                        .Throws(new Exception("Error"));
                    break;
            }
            //Act
            IActionResult response = await _currentController.AddPropiedad(It.IsAny<PropiedadRequest>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 404)]
        public async Task UpdatePropiedadTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _propiedadService
                        .Setup(t => t.UpdatePropiedad(It.IsAny<PropiedadModRequest>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _propiedadService
                        .Setup(t => t.UpdatePropiedad(It.IsAny<PropiedadModRequest>()))
                        .Throws(new Exception("Error"));
                    break;
                case 3:
                    _propiedadService
                        .Setup(t => t.UpdatePropiedad(It.IsAny<PropiedadModRequest>()))
                        .Returns(Task.FromResult(false));
                    break;
            }
            //Act
            IActionResult response = await _currentController.UpdatePropiedad(It.IsAny<PropiedadModRequest>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 404)]
        public async Task DeletePropiedadTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _propiedadService
                        .Setup(t => t.DeletePropiedad(It.IsAny<int>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _propiedadService
                        .Setup(t => t.DeletePropiedad(It.IsAny<int>()))
                        .Throws(new Exception("Error"));
                    break;
                case 3:
                    _propiedadService
                        .Setup(t => t.DeletePropiedad(It.IsAny<int>()))
                        .Returns(Task.FromResult(false));
                    break;
            }
            //Act
            IActionResult response = await _currentController.DeletePropiedad(It.IsAny<int>());
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        public async Task GetAllPropiedadsTest(int index, int expected)
        {

            //Arrange
            switch (index)
            {
                case 1:
                    _propiedadService
                        .Setup(t => t.GetAllPropiedad())
                        .Returns(Task.FromResult(It.IsAny<List<PropiedadModRequest>>()));
                    break;
                case 2:
                    _propiedadService
                        .Setup(t => t.GetAllPropiedad())
                        .Throws(new Exception("Error"));
                    break;
            }
            //Act
            IActionResult response = await _currentController.GetAllPropiedades();
            var result = response as ObjectResult;

            //Assert
            Assert.Equal(result.StatusCode, expected);
        }
    }
}