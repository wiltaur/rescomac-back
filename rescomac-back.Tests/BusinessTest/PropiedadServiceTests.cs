using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using rescomac_back.business.Implement;
using rescomac_back.repository.Context;
using rescomac_back.repository.Dto;
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
    public class PropiedadServiceTests : RescomacDbContextMock
    {
        private PropietarioService _currentService;
        private readonly Mock<RescomacDbContext> _context;


        private Fixture _autodata;

        public PropiedadServiceTests()
        {
            _autodata = new Fixture();
            _autodata.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _autodata.Behaviors.Remove(b));
            _autodata.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _context = GetDbContextMock();
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public async Task CreatePropiedadTest(int index, bool expected)
        {
            var prop = _autodata.Create<PropiedadRequest>();
            var mockSet = new Mock<DbSet<Propiedad>>();
            _context
                .Setup(t => t.Propiedads)
                .Returns(mockSet.Object);
            //Arrange
            switch (index)
            {
                case 1:
                    _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
                    break;
                case 2:
                    _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
                    break;
            }
            //Act
            _currentService = new PropietarioService(
                                        _context.Object);

            bool response = await _currentService.AddPropiedad(prop);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Propiedad>()), Times.Once());
            _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(response, expected);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        [InlineData(3, false)]
        public async Task UpdatePropiedadTest(int index, bool expected)
        {
            var productRequest = _autodata.Create<PropiedadModRequest>();
            var productDb = _autodata.Create<Propiedad>();
            productDb.Id = 1;

            _context.Setup(c => c.Propiedads).Returns(GetQueryableMockDbSet(productDb));

            //Arrange
            switch (index)
            {
                case 1:
                    productRequest.Id = 1;
                    _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
                    break;
                case 2:
                    productRequest.Id = 1;
                    _context.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);
                    break;
                case 3:
                    productRequest.Id = 2;
                    break;
            }
            //Act
            _currentService = new PropietarioService(
                                        _context.Object);

            bool response = await _currentService.UpdatePropiedad(productRequest);

            //Assert
            if (index != 3)
                _context.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
            Assert.Equal(response, expected);
        }
    }
}
