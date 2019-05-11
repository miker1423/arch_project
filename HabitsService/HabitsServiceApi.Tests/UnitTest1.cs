using HabitsServiceApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HabitsServiceApi.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateHabit()
        {
            var options = new DbContextOptionsBuilder<HabitsContext>().UseInMemoryDatabase(databaseName: "habitsDb").Options;

            using (var context = new HabitsContext(options))
            {
                Assert.AreEqual(1, context.Habits.Count());
            }
        }
    }
}
