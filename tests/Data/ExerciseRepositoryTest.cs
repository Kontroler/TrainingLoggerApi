using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TrainingLogger.API.Data;
using TrainingLogger.API.Models;
using TrainingLogger.Data;
using TrainingLogger.Models;

namespace Tests.Data
{
    [TestFixture]
    public abstract class ExerciseRepositoryTest
    {
        #region Seeding
        protected ExerciseRepositoryTest(DbContextOptions<DataContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }

        protected DbContextOptions<DataContext> ContextOptions { get; }

        [OneTimeSetUp]
        public async Task Seed()
        {
            using var context = new DataContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            await AddUser(context);
        }
        #endregion

        #region User methods
        private async Task AddUser(DataContext context)
        {
            var authRepo = new AuthRepository(context);
            var user = new User { Username = "User" };
            await authRepo.Register(user, "123456");
        }

        private async Task<User> GetUser(DataContext context)
        {
            var authRepo = new UserRepository(context);
            return await authRepo.GetUser(1);
        }
        #endregion

        #region Exercise methods
        private async Task AddExercise(string exerciseName, IExerciseRepository repo, DataContext context)
        {
            var exercise = Exercise.Create(exerciseName, await GetUser(context));
            repo.Add(exercise);
            await repo.SaveAll();
        }
        #endregion

        #region CadAdd
        [Test]
        public async Task CanAdd()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);

            var exerciseRepo = new ExerciseRepository(context);
            var countBeforeAdd = await exerciseRepo.GetAllByUserId(user.Id);
            await AddExercise("Exercise test CanAdd", exerciseRepo, context);
            var countAfterAdd = await exerciseRepo.GetAllByUserId(user.Id);

            Assert.AreNotSame(countBeforeAdd, countAfterAdd);
        }
        #endregion


        #region CadAddNull_ShouldThrowArgumentNullException
        [Test]
        public void CadAddNull_ShouldThrowArgumentNullException()
        {
            using var context = new DataContext(ContextOptions);
            var exerciseRepo = new ExerciseRepository(context);

            Assert.Throws<ArgumentNullException>(() => exerciseRepo.Add(null));
        }
        #endregion

        #region CanGetByUserId
        [Test]
        public async Task CanGetByUserId()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);

            var exerciseRepo = new ExerciseRepository(context);
            await AddExercise("Exercise test CanGetByUserId", exerciseRepo, context);
            var exercises = await exerciseRepo.GetAllByUserId(user.Id);
            Assert.IsNotEmpty(exercises);
        }
        #endregion

        #region CanGetByName
        [Test]
        public async Task CanGetByName()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);

            var exerciseRepo = new ExerciseRepository(context);
            var exerciseName = "Exercise test CanGetByName";
            await AddExercise(exerciseName, exerciseRepo, context);

            var exercisesReturned = await exerciseRepo.GetByName(exerciseName, user.Id);
            Assert.AreEqual(exerciseName, exercisesReturned.Name);
        }
        #endregion

        #region CanDelete
        [Test]
        public async Task CanDelete()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);

            var exerciseRepo = new ExerciseRepository(context);
            var exerciseName ="Exercise test CanDelete"; 
            await AddExercise(exerciseName, exerciseRepo, context);
            var exercise = (await exerciseRepo.GetAllByUserId(user.Id)).FirstOrDefault(x => x.Name == exerciseName);
            Assert.IsNotNull(exercise);

            // Delete exercise
            exerciseRepo.Delete(exercise);
            await exerciseRepo.SaveAll();

            // Check is deleted
            var exercises = await exerciseRepo.GetAllByUserId(user.Id);
            Assert.IsFalse(exercises.Any(x => x.Name == exerciseName));
        }
        #endregion
    }
}