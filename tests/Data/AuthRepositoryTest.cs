using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TrainingLogger.API.Data;
using TrainingLogger.API.Models;

namespace Tests.Data
{
    public abstract class AuthRepositoryTest
    {
        #region Seeding
        protected AuthRepositoryTest(DbContextOptions<DataContext> contextOptions)
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

        #region CanRegister
        [Test]
        public async Task CanRegister()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new AuthRepository(context);

            var user = new User { Username = "TestName" };

            var createdUser = await repo.Register(user, "123456");

            Assert.AreEqual(1, createdUser.Id);
            Assert.AreEqual("TestName", createdUser.Username);
            Assert.IsNull(createdUser.LastActive);
        }
        #endregion

        #region CanLogin
        [Test]
        public async Task CanLogin_Success()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new AuthRepository(context);

            var user = new User { Username = "TestName" };
            var createdUser = await repo.Register(user, "123456");

            var loggedUser = await repo.Login("TestName", "123456");
            Assert.IsNotNull(loggedUser);
            Assert.AreEqual("TestName", loggedUser.Username);
            Assert.IsNotNull(loggedUser.LastActive);
        }

        [Test]
        public async Task CanLogin_Failure()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new AuthRepository(context);

            var user = new User { Username = "TestName" };
            var createdUser = await repo.Register(user, "123456");

            var loggedUser = await repo.Login("TestName", "1234567");
            Assert.IsNull(loggedUser);
        }
        #endregion

        #region CanCheckUserExists
        [Test]
        public async Task CanCheckUserExists_UserExists()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new AuthRepository(context);

            var user = new User { Username = "TestName" };
            var createdUser = await repo.Register(user, "123456");

            var exist = await repo.UserExists("TestName");
            Assert.IsTrue(exist);
        }

        [Test]
        public async Task CanCheckUserExists_UserNotExists()
        {
            using var context = new DataContext(ContextOptions);
            var repo = new AuthRepository(context);

            var exist = await repo.UserExists("TestName");
            Assert.IsFalse(exist);
        }
        #endregion
    }
}