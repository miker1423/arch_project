using HabitsServiceApi.Controllers;
using HabitsServiceApi.Interfaces;
using HabitsServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace HabitsServiceApi.Tests
{
    [TestClass]
    public class HabitsServiceApiTest
    {
        private Guid UserId_1 = new Guid("00000000-0000-0000-0000-000000000001");
        private Guid UserId_2 = new Guid("00000000-0000-0000-0000-000000000002");

        private Guid HabitId_1 = new Guid("00000000-0000-0000-0000-000000000003");
        private Guid HabitId_2 = new Guid("00000000-0000-0000-0000-000000000004");

        [TestMethod]
        public void CreateHabit_Success()
        {
            //Arrange
            Mock<IHabitsService> habitsService = new Mock<IHabitsService>();

            Habit habitToCreate = new Habit(0, 0, 2, "Description", "Title", HabitId_1, UserId_1);
            habitsService.Setup(service => service.CreateHabit(new Habit())).Returns(HabitId_1);
            HabitsController habitsController = new HabitsController(habitsService.Object);
            //Act
            IActionResult result = habitsController.Post(habitToCreate);
            //Assert
            habitsService.Verify(service => service.CreateHabit(habitToCreate));
            //Assert.Equals(result);
        }

        [TestMethod]
        public void UpdateHabit_Success()
        {

        }

        [TestMethod]
        public void DeleteHabit_Success()
        {

        }

        [TestMethod]
        public void GetHabitsByUserId_Success()
        {

        }

        [TestMethod]
        public void GetHabitByHabitId_Success()
        {

        }

        [TestMethod]
        public void GetAllHabits_Success()
        {

        }

    }
}
