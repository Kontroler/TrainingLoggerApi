using System;
using NUnit.Framework;
using TrainingLogger.API.Models;
using TrainingLogger.Models;

namespace tests.Models
{
    public class ExerciseTest
    {
        [Test]
        public void Test()
        {
            var user = new User {
                Id = 1,
                Username = "Test",
                PasswordHash = new byte[0],
                PasswordSalt = new byte[0],
                Created = new DateTime(2020, 7, 18),
                LastActive = new DateTime(2020, 7, 20)
            };
            var exercise = Exercise.Create("Exercise test", user);

            Assert.AreEqual(0,  exercise.Id);
            Assert.AreEqual("Exercise test", exercise.Name);
            Assert.IsNotNull(exercise.Created);
            Assert.IsNotNull(exercise.LastUpdated);
            Assert.AreEqual(1, exercise.User.Id);
            Assert.AreEqual(1, exercise.CreatedBy.Id);
            Assert.AreEqual(1, exercise.LastUpdatedBy.Id);
        }
    }
}