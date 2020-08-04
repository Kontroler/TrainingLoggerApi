using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;

namespace Tests.Data
{
    public class InMemoryUnitsControllerTest : UnitsRepositoryTest 
    {
        public InMemoryUnitsControllerTest()
            : base(
                new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}