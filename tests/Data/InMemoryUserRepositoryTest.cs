using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;

namespace Tests.Data
{
    public class InMemoryUserRepositoryTest : UserRepositoryTest
    {
        public InMemoryUserRepositoryTest()
            : base(
                new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}