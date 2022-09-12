using Microsoft.EntityFrameworkCore;
using Moq;
using rescomac_back.repository.Context;

namespace rescomac_back.Test.MockDbContext
{
    public class RescomacDbContextMock
    {
        public static Mock<RescomacDbContext> GetDbContext()
        {
            var dbName = Guid.NewGuid().ToString();
            var dbOptions = new DbContextOptionsBuilder<RescomacDbContext>()
                        .UseInMemoryDatabase(dbName)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .EnableSensitiveDataLogging(true)
                        .Options;
            return new Mock<RescomacDbContext>(dbOptions);
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
