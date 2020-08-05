using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;

namespace Tests.Data
{
    public class InMemoryExerciseRepositoryTest : ExerciseRepositoryTest
    {
        public InMemoryExerciseRepositoryTest()
            : base(
                new DbContextOptionsBuilder<DataContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options)
        {
        }
    }
}