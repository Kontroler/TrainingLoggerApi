using NUnit.Framework;
using TrainingLogger.Models;

namespace Tests
{
    public class UnitTest1
    {
        [SetUp]
        public void Setup()
        {
            var a = new Training();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}