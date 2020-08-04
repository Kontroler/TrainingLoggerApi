using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;

namespace Tests.Data
{
    public class InMemoryAuthRepositoryTest : AuthRepositoryTest
    {
        public InMemoryAuthRepositoryTest()
            : base(
                new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}