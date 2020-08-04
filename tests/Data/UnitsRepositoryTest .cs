using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using TrainingLogger.API.Data;
using TrainingLogger.Data;
using TrainingLogger.Models;

namespace Tests.Data
{
    public abstract class Test1
    {
        #region Seeding
        protected Test1(DbContextOptions<DataContext> contextOptions)
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

            context.SaveChanges();
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
        public void CanAdd()
        {
            using var context = new DataContext(ContextOptions);
            var repository = new UnitRepository(context);
            var unit = new Unit { Code = "pcs." };
            
            repository.Add(unit);

            Assert.AreEqual(3, unit.Id);
            Assert.AreEqual("pcs.", unit.Code);
        }
        #endregion

        #region CanGetAllEndDelete
        [Test]
        public async Task CanGetAllEndDelete()
        {
            using var context = new DataContext(ContextOptions);
            var repository = new UnitRepository(context);
            
            var units = await repository.GetAll();
            Assert.AreEqual(2, units.Count());


            repository.Delete(units.ElementAt(0));
            var saved = await repository.SaveAll();
            Assert.IsTrue(saved);


            units = await repository.GetAll();
            Assert.AreEqual(1, units.Count());
        }
        #endregion

    }
}