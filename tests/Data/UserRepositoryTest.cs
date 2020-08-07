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

        [OneTimeSetUp]
        public void Seed()
        {
            using var context = new DataContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        #endregion

        #region User methods
        private async Task<User> AddUser(string username, IUserRepository repo)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = new byte[0],
                PasswordSalt = new byte[0],
                Created = new System.DateTime(2020, 1, 1),
                LastActive = null
            };
            repo.Add(user);
            await repo.SaveAll();

            return user;
        }
        #endregion

        #region CanAdd
        [Test]
        public async Task CanAdd()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new UserRepository(context);

            var user = new User
            {
                Username = "TestName CanAdd",
                PasswordHash = new byte[0],
                PasswordSalt = new byte[0],
                Created = new System.DateTime(2020, 1, 1),
                LastActive = null
            };
            repo.Add(user);
            var saved = await repo.SaveAll();
            Assert.IsTrue(saved);

        }
        #endregion

        #region CanDelete
        [Test]
        public async Task CanDelete()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new UserRepository(context);

            var user = await AddUser("TestName CanDelete", repo);

            repo.Delete(user);
            await repo.SaveAll();

            var userReturned = await repo.GetUser(user.Id);
            Assert.IsNull(userReturned);
        }
        #endregion

        #region CanGetUser
        [Test]
        public async Task CanGetUser()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new UserRepository(context);

            var user = await AddUser("TestName CanGetUser", repo);

            var userReturned = await repo.GetUser(user.Id);
            Assert.IsNotNull(userReturned);
            Assert.AreEqual("TestName CanGetUser", userReturned.Username);
            Assert.IsNull(userReturned.LastActive);
        }
        #endregion

    }
}