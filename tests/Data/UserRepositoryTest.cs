using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TrainingLogger.API.Data;
using TrainingLogger.API.Models;

namespace Tests.Data
{
    public abstract class UserRepositoryTest
    {
        #region Seeding
        protected UserRepositoryTest(DbContextOptions<DataContext> contextOptions)
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

        #region CanAdd
        [Test]
        public void CanAdd()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new UserRepository(context);

            var user = new User { Username = "TestName" };
            repo.Add(user);

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("TestName", user.Username);
            Assert.IsNull(user.LastActive);
        }
        #endregion

        #region CanDelete
        [Test]
        public async Task CanDelete()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new UserRepository(context);

            var user = new User { Username = "TestName" };
            repo.Add(user);
            await repo.SaveAll();

            Assert.AreEqual(1, user.Id);
            Assert.AreEqual("TestName", user.Username);
            Assert.IsNull(user.LastActive);

            repo.Delete(user);
            await repo.SaveAll();
            
            var userReturned = await repo.GetUser(1);
            Assert.IsNull(userReturned);
        }
        #endregion

        #region CanGetUser
        [Test]
        public async Task CanGetUser()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new UserRepository(context);

            var user = new User { Username = "TestName" };
            repo.Add(user);
            await repo.SaveAll();
            
            var userReturned = await repo.GetUser(1);
            Assert.AreEqual(1, userReturned.Id);
            Assert.AreEqual("TestName", userReturned.Username);
            Assert.IsNull(userReturned.LastActive);
        }
        #endregion

    }
}