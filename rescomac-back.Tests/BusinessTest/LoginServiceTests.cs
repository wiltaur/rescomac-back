using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using rescomac_back.business.Implement;
using rescomac_back.repository.Context;
using rescomac_back.repository.Model;
using rescomac_back.Tests.MockDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace rescomac_back.Tests.BusinessTest
{
    public class LoginServiceTests : RescomacDbContextMock
    {
        private LoginService _currentService;
        private readonly RescomacDbContext _context;

        private Fixture _autodata;

        public LoginServiceTests()
        {
            _autodata = new Fixture();
            _autodata.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _autodata.Behaviors.Remove(b));
            _autodata.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _context = GetDbContext();
            _currentService = new LoginService(_context);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public async Task ValidarUsuarioTest(int index, bool expected)
        {
            //Arrange
            var user = _autodata.Create<Usuario>();

            _context.Add(user);
            _context.SaveChanges();

            bool response = false;
            
            //Act
            switch (index)
            {
                case 1:
                    response = await _currentService.ValidarUsuario(user.Correo, user.Contresena);
                    break;
                case 2:
                    response = await _currentService.ValidarUsuario(user.Correo+"1", user.Contresena);
                    break;
            }

            //Assert
            Assert.Equal(response, expected);
        }
    }
}
