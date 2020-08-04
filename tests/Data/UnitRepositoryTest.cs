using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TrainingLogger.Data;
using TrainingLogger.Models;

namespace Tests.Data
{
    public class UnitRepositoryTest
    {
        [Test]
        public void Test_GetByCode_kg()
        {
            var units = new List<Unit>
            {
                new Unit { Id = 1, Code = "kg" },
                new Unit { Id = 2, Code = "lbs" }
            };

            var unitRepository = new Mock<IUnitRepository>();
            unitRepository.Setup(u => u.GetByCode("kg")).Returns(Task.FromResult(units[0]));

            var testUnit = unitRepository.Object.GetByCode("kg").Result;

            Assert.AreEqual(1, testUnit.Id);
            Assert.AreEqual("kg", testUnit.Code);
        }

    }
}