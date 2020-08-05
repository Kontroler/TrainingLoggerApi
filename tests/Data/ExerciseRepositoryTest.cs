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
    public abstract class ExerciseRepositoryTest
    {
        #region Seeding
        protected ExerciseRepositoryTest(DbContextOptions<DataContext> contextOptions)
        {
            ContextOptions = contextOptions;
        }

        protected DbContextOptions<DataContext> ContextOptions { get; }

        [SetUp]
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

        #region CadAdd
        [Test]
        public async Task CanAdd()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);

            var exerciseRepo = new ExerciseRepository(context);
            var exercise = new Exercise { Name = "Exercise test", User = user };
            exerciseRepo.Add(exercise);

            Assert.AreEqual(1, exercise.Id);
            Assert.AreEqual("Exercise test", exercise.Name);
            Assert.AreEqual(1, exercise.User.Id);
            Assert.AreEqual("User", exercise.User.Username);
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

            var exercises = await exerciseRepo.GetAllByUserId(user.Id);
            Assert.IsEmpty(exercises);

            // Add new exercise
            var exercise = new Exercise { Name = "Exercise test", User = user };
            exerciseRepo.Add(exercise);
            await exerciseRepo.SaveAll();

            // Check is added
            exercises = await exerciseRepo.GetAllByUserId(user.Id);
            Assert.AreEqual(1, exercises.Count());
        }
        #endregion

        #region CanGetByName
        [Test]
        public async Task CanGetByName()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);

            var exerciseRepo = new ExerciseRepository(context);

            var exercises = await exerciseRepo.GetAllByUserId(user.Id);
            Assert.IsEmpty(exercises);

            // Add new exercise
            var exercise = new Exercise { Name = "Exercise test", User = user };
            exerciseRepo.Add(exercise);
            await exerciseRepo.SaveAll();

            // Check is added
            var exercisesReturned = await exerciseRepo.GetByName("Exercise test", user.Id);
            Assert.AreEqual("Exercise test", exercisesReturned.Name);
        }
        #endregion

        #region CadDelete
        [Test]
        public async Task CanDelete()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);

            var exerciseRepo = new ExerciseRepository(context);

            // Add new exercise
            var exercise = new Exercise { Name = "Exercise test", User = user };
            exerciseRepo.Add(exercise);
            await exerciseRepo.SaveAll();

            // Check is added
            var exercises = await exerciseRepo.GetAllByUserId(user.Id);
            Assert.AreEqual(1, exercises.Count());

            // Delete exercise
            exerciseRepo.Delete(exercise);
            await exerciseRepo.SaveAll();

            // Check is deleted
            exercises = await exerciseRepo.GetAllByUserId(user.Id);
            Assert.IsEmpty(exercises);
        }
        #endregion
    }
}