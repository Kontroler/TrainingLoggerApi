using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TrainingLogger.API.Data;
using TrainingLogger.Data;

namespace Tests.Services
{
    public abstract class TrainingsServiceTest
    {
        #region Seeding
        protected TrainingsServiceTest(DbContextOptions<DataContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }

        protected DbContextOptions<DataContext> ContextOptions { get; }

        [SetUp]
        public void Seed()
        {
            using var context = new DataContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        #endregion

    }
}