using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TrainingLogger.API.Data;
using TrainingLogger.Data;
using TrainingLogger.Models;

namespace Tests.Data
{
    [TestFixture]
    public abstract class UnitsRepositoryTest
    {
        #region Seeding
        protected UnitsRepositoryTest(DbContextOptions<DataContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<DataContext> ContextOptions { get; }
        
        public void Seed()
        {
            using var context = new DataContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
        #endregion

        #region CanGetByCodeKg
        [Test]
        public void CanGetByCodeKg()
        {
            using var context = new DataContext(ContextOptions);
            var repository = new UnitRepository(context);
            var unitKg = repository.GetByCode("kg").Result;

            Assert.AreEqual(1, unitKg.Id);
            Assert.AreEqual("kg", unitKg.Code);
        }
        #endregion

        #region CanGetByCodeLbs
        [Test]
        public void CanGetByCodeLbs()
        {
            using var context = new DataContext(ContextOptions);
            var repository = new UnitRepository(context);
            var unitKg = repository.GetByCode("lbs").Result;

            Assert.AreEqual(2, unitKg.Id);
            Assert.AreEqual("lbs", unitKg.Code);
        }
        #endregion

        #region CanAdd
        [Test]
        public async Task CanAdd()
        {
            using var context = new DataContext(ContextOptions);
            var repository = new UnitRepository(context);
            var unit = new Unit { Code = "pcs." };

            repository.Add(unit);
            await repository.SaveAll();

            var unitPcs = await repository.GetByCode("pcs.");
            Assert.AreEqual(3, unitPcs.Id);
            Assert.AreEqual("pcs.", unitPcs.Code);
        }
        #endregion

        #region CanDelete
        [Test]
        public async Task CanDelete()
        {
            using var context = new DataContext(ContextOptions);
            var repository = new UnitRepository(context);

            var units = await repository.GetByCode("pcs.");
            Assert.NotNull(units);

            repository.Delete(units);
            await repository.SaveAll();

            Assert.False(context.Set<Unit>().Any(u => u.Code == "pcs."));
        }
        #endregion

    }
}