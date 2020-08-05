using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;

namespace Tests.Data
{
    public class InMemoryTrainingRepositoryTest: TrainingRepositoryTest
    {
         public InMemoryTrainingRepositoryTest()
            : base(
                new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}