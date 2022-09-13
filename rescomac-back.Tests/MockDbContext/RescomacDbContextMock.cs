using EntityFrameworkCore.Testing.Moq.Helpers;
using Microsoft.EntityFrameworkCore;
using Moq;
using rescomac_back.repository.Context;

namespace rescomac_back.Tests.MockDbContext
{
    public class RescomacDbContextMock
    {
        protected RescomacDbContext GetDbContext()
        {
            var dbName = Guid.NewGuid().ToString();
            var opciones = new DbContextOptionsBuilder<RescomacDbContext>()
                .UseInMemoryDatabase(dbName)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(true)
                .Options;

            var dbContextToMock = new RescomacDbContext(opciones);
            var mockedDbContext = new MockedDbContextBuilder<RescomacDbContext>()
                .UseDbContext(dbContextToMock)
                .UseConstructorWithParameters(opciones)
                .MockedDbContext;

            return mockedDbContext;
        }

        public static DbSet<T> GetQueryableMockDbSet<T>(params T[] sourceList) where T : class
        {
            return GetMockDbSet(sourceList).Object;
        }

        public static Mock<DbSet<T>> GetMockDbSet<T>(params T[] sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet;
        }
    }
}