using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TrainingLogger.API.Data;
using TrainingLogger.API.Models;
using TrainingLogger.Data;
using TrainingLogger.Models;

namespace Tests.Data
{
    public abstract class TrainingRepositoryTest
    {
        #region Seeding
        protected TrainingRepositoryTest(DbContextOptions<DataContext> contextOptions)
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
            await AddExercise(context);
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
        private async Task AddExercise(DataContext context)
        {
            var repo = new ExerciseRepository(context);
            var exercise = Exercise.Create("Exercise 1", await GetUser(context));
            repo.Add(exercise);
            await repo.SaveAll();
        }

        private async Task<Exercise> GetExercise(DataContext context)
        {
            var repo = new ExerciseRepository(context);
            return await repo.GetByName("Exercise 1", 1);
        }
        #endregion

        #region Unit methods
        private async Task<Unit> GetUnit(DataContext context)
        {
            var repo = new UnitRepository(context);
            return await repo.GetByCode("kg");
        }
        #endregion

        #region CadAdd
        [Test]
        public async Task CanAdd()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);
            var trainingRepo = new TrainingRepository(context);


            trainingRepo.Add(await CreateTraining(context, user));
            await trainingRepo.SaveAll();

            var trainingsReturned = await trainingRepo.GetAllByUserId(user.Id);

            var training0 = trainingsReturned.ElementAt(0);
            Assert.AreEqual(1, trainingsReturned.Count());
            Assert.AreEqual(1, training0.Id);
            Assert.AreEqual(1, training0.Exercises.Count());
            Assert.AreEqual("Training 1", training0.Name);
            Assert.AreEqual("User", training0.User.Username);

            var training0Exercise0 = training0.Exercises.ElementAt(0);
            Assert.AreEqual(1, training0Exercise0.Id);
            Assert.AreEqual("Exercise 1", training0Exercise0.Exercise.Name);
            Assert.AreEqual(1, training0Exercise0.Sets.Count());

            var trainig0Exercise0Set0 = training0Exercise0.Sets.ElementAt(0);
            Assert.AreEqual(1, trainig0Exercise0Set0.Id);
            Assert.AreEqual(2, trainig0Exercise0Set0.Reps.Count());

            var trainig0Exercise0Set0Rep0 = trainig0Exercise0Set0.Reps.ElementAt(0);
            Assert.AreEqual(1, trainig0Exercise0Set0Rep0.Id);
            Assert.AreEqual(10, trainig0Exercise0Set0Rep0.Value);
            Assert.AreEqual(80, trainig0Exercise0Set0Rep0.Weight);
            Assert.AreEqual("kg", trainig0Exercise0Set0Rep0.Unit.Code);

            var trainig0Exercise0Set0Rep1 = trainig0Exercise0Set0.Reps.ElementAt(1);
            Assert.AreEqual(2, trainig0Exercise0Set0Rep1.Id);
            Assert.AreEqual(10, trainig0Exercise0Set0Rep1.Value);
            Assert.AreEqual(85, trainig0Exercise0Set0Rep1.Weight);
            Assert.AreEqual("kg", trainig0Exercise0Set0Rep1.Unit.Code);
        }

        #endregion

        #region CanDelete
        public async Task CanDelete()
        {
            using var context = new DataContext(ContextOptions);
            var user = await GetUser(context);
            var trainingRepo = new TrainingRepository(context);
            var training = await CreateTraining(context, user);
            trainingRepo.Add(training);
            var saved = await trainingRepo.SaveAll();
            Assert.IsTrue(saved);
            
            var trainingsReturned = await trainingRepo.GetAllByUserId(user.Id);
            Assert.AreEqual(1, trainingsReturned.Count());

            trainingRepo.Delete(trainingsReturned.ElementAt(0));
            await trainingRepo.SaveAll();

            var trainingsReturnedAfterDelete = await trainingRepo.GetAllByUserId(user.Id);
            Assert.IsEmpty(trainingsReturnedAfterDelete);

            var trainingExercisesAfterDelete = await context.TrainingExercises.ToListAsync();
            Assert.IsEmpty(trainingExercisesAfterDelete);

            var trainingExerciseSetsAfterDelete = await context.TrainingExerciseSets.ToListAsync();
            Assert.IsEmpty(trainingExerciseSetsAfterDelete);

            var trainingExerciseSetRepsAfterDelete = await context.TrainingExerciseSetReps.ToListAsync();
            Assert.IsEmpty(trainingExerciseSetRepsAfterDelete);
        }
        #endregion

        private async Task<Training> CreateTraining(DataContext context, User user)
        {
            var training = Training.Create(
                "Training 1",
                new DateTime(2020, 7, 20),
                user
            );

            var exercise = await GetExercise(context);
            var trainingExercise = TrainingExercise.Create(exercise, user);

            var trainigExerciseSet = TrainingExerciseSet.Create(user);

            var unit = await GetUnit(context);
            var trainingExerciseSetRep1 = TrainingExerciseSetRep.Create(
                10,
                80,
                unit,
                user
            );

            var trainingExerciseSetRep2 = TrainingExerciseSetRep.Create(
                10,
                85,
                unit,
                user
            );

            trainigExerciseSet.Reps.Add(trainingExerciseSetRep1);
            trainigExerciseSet.Reps.Add(trainingExerciseSetRep2);
            trainingExercise.Sets.Add(trainigExerciseSet);
            training.Exercises.Add(trainingExercise);
            return training;
        }
    }
}