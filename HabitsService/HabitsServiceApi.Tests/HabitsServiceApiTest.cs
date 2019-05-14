using HabitsServiceApi.Controllers;
using HabitsServiceApi.Interfaces;
using HabitsServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RabbitWrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HabitsServiceApi.Tests
{
    [TestClass]
    public class HabitsServiceApiTest
    {
        private Guid UserId_1 = new Guid("00000000-0000-0000-0000-000000000001");
        private Guid UserId_2 = new Guid("00000000-0000-0000-0000-000000000002");

        private Guid HabitId_1 = new Guid("00000000-0000-0000-0000-000000000003");
        private Guid HabitId_2 = new Guid("00000000-0000-0000-0000-000000000004");

        private Wrapper _rabbitWrapper;

        [TestMethod]
        public void CreateHabit_Success()
        {
            //Arrange
            Mock<IHabitsService> habitsService = new Mock<IHabitsService>();
            _rabbitWrapper = new Wrapper("TEST_URL");
            Habit habitToCreate = new Habit(0, 0, 2, "Description", "Title", HabitId_1, UserId_1);
            habitsService.Setup(service => service.CreateHabit(new Habit())).Returns(HabitId_1);
            HabitsController habitsController = new HabitsController(habitsService.Object, _rabbitWrapper);

            //Ac
            var result = habitsController.PostTest(habitToCreate) as CreatedResult;
            //Assert
            habitsService.Verify(service => service.CreateHabit(habitToCreate));
            Assert.AreEqual(201, result.StatusCode);
        }

        [TestMethod]
        public void UpdateHabit_Success()
        {
            //Arrange
            Mock<IHabitsService> habitsService = new Mock<IHabitsService>();
            _rabbitWrapper = new Wrapper("TEST_URL");
            Habit habitToCreate = new Habit(0, 0, 2, "Description", "Title", HabitId_1, UserId_1);
            habitsService.Setup(service => service.UpdateHabit(habitToCreate)).Returns(habitToCreate);
            HabitsController habitsController = new HabitsController(habitsService.Object, _rabbitWrapper);

            //Ac
            var result = habitsController.Put(habitToCreate) as OkObjectResult;
            //Assert
            habitsService.Verify(service => service.UpdateHabit(habitToCreate));
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void DeleteHabit_Success()
        {
            //Arrange
            Mock<IHabitsService> habitsService = new Mock<IHabitsService>();
            _rabbitWrapper = new Wrapper("TEST_URL");
            Habit habitToDelete = new Habit(0, 0, 2, "Description", "Title", HabitId_1, UserId_1);
            habitsService.Setup(service => service.CreateHabit(habitToDelete)).Returns(HabitId_1);
            HabitsController habitsController = new HabitsController(habitsService.Object, _rabbitWrapper);

            //Act
            var result = habitsController.Delete(HabitId_1.ToString()) as OkObjectResult;
            //Assert
            habitsService.Verify(service => service.DeleteHabit(HabitId_1));
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void GetHabitsByUserId_Success()
        {
            //Arrange
            Mock<IHabitsService> habitsService = new Mock<IHabitsService>();
            _rabbitWrapper = new Wrapper("TEST_URL");
            habitsService.Setup(service => service.GetHabitsByUserId(UserId_1)).Returns(new Collection<Habit>());            
            HabitsController habitsController = new HabitsController(habitsService.Object);

            //Act
            var result = habitsController.GetHabitsByUserId(UserId_1.ToString());
            //Assert
            habitsService.Verify(service => service.GetHabitsByUserId(UserId_1));
        }

        [TestMethod]
        public void GetHabitByHabitId_Success()
        {
            //Arrange
            Mock<IHabitsService> habitsService = new Mock<IHabitsService>();
            _rabbitWrapper = new Wrapper("TEST_URL");
            Habit habitToCreate = new Habit(0, 0, 2, "Description", "Title", HabitId_1, UserId_1);
            habitsService.Setup(service => service.GetHabit(HabitId_1)).Returns(habitToCreate);

            HabitsController habitsController = new HabitsController(habitsService.Object);

            //Act
            var result = habitsController.Get(HabitId_1.ToString()) as OkObjectResult;
            //Assert
            habitsService.Verify(service => service.GetHabit(HabitId_1));
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void GetAllHabits_Success()
        {
            //Arrange
            Mock<IHabitsService> habitsService = new Mock<IHabitsService>();
            _rabbitWrapper = new Wrapper("TEST_URL");
            Habit habitToCreate = new Habit(0, 0, 2, "Description", "Title", HabitId_1, UserId_1);
            habitsService.Setup(service => service.GetAllHabits()).Returns(new Collection<Habit>());

            HabitsController habitsController = new HabitsController(habitsService.Object);

            //Act
            var result = habitsController.Get() as OkObjectResult;
            //Assert
            habitsService.Verify(service => service.GetAllHabits());
            Assert.AreEqual(200, result.StatusCode);
        }

    }
}
