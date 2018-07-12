using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDoList.App_Code;
using ToDoList.Models;
using System.Linq;
using System.Collections.Generic;
using ToDoList.Controllers;
using System.Web.Mvc;

namespace ToDoList.Tests
{
    [TestClass]
    public class TODOUnitTest
    {
        [TestMethod]
        public void Unit_Test_Details()
        {
            #region Arrange Part            
            Mock<IBusinessDataAccount> todoMock = new Mock<IBusinessDataAccount>();
            List<ToDoModel> mockToDoModel = new List<ToDoModel>();
            mockToDoModel.Add(new ToDoModel
            {
                ID = 1,
                TaskName = "Test Task 1",
                TaskDescription = "Test Task 1 Description",
                Username = "Test1",
                IsDone = false
            });
            mockToDoModel.Add(new ToDoModel
            {
                ID = 2,
                TaskName = "Test Task 2",
                TaskDescription = "Test Task 2 Description",
                Username = "Test1",
                IsDone = false
            });
            todoMock.Setup(m => m.Details("Test1")).Returns(mockToDoModel);
            
            ToDosController testController = new ToDosController(todoMock.Object);
            
            #endregion            

            #region Act Part
            var actual = testController.Details() as ViewResult;
            #endregion

            #region Assert Part
            Assert.IsInstanceOfType(actual.Model, typeof(List<ToDoModel>));
            #endregion

        }
        
        [TestMethod]
        public void Unit_Test_CreateModal()
        {
            #region Arrange Part
            Mock<IBusinessDataAccount> todoMock = new Mock<IBusinessDataAccount>();
            ToDosController testController = new ToDosController(todoMock.Object);
            #endregion

            #region Act Part
            var actual = (PartialViewResult)testController.CreateModal();
            #endregion

            #region Assert Part
            Assert.AreEqual("_CreateTaskModal", actual.ViewName);
            #endregion
        }

        [TestMethod]
        public void Unit_Test_SaveNewTask()
        {
            #region Arrange Part            
            Mock<IBusinessDataAccount> todoMock = new Mock<IBusinessDataAccount>();
            ToDoModel mockToDoModel = new ToDoModel
            {
                TaskName = "Test Task 1",
                TaskDescription = "Test Task 1 Description",
                Username = "Test1"
            };

            todoMock.Setup(m => m.SaveNewTask(mockToDoModel)).Returns(mockToDoModel);

            ToDosController testController = new ToDosController(todoMock.Object);
            #endregion

            #region Act Part
            var actual = testController.SaveNewTask(mockToDoModel) as PartialViewResult;
            #endregion

            #region Assert Part
            Assert.AreEqual("_SaveTask", actual.ViewName);
            #endregion
        }

        [TestMethod]
        public void Unit_Test_MarkAsCompleted()
        {
            #region Arrange Part            
            Mock<IBusinessDataAccount> todoMock = new Mock<IBusinessDataAccount>();

            todoMock.Setup(m => m.MarkAsCompleted(1, "Test1")).Returns(1);

            ToDosController testController = new ToDosController(todoMock.Object);
            #endregion

            #region Act Part
            var actual = testController.MarkAsCompleted(1) as EmptyResult;
            #endregion

            #region Assert Part
            Assert.IsInstanceOfType(actual, typeof(EmptyResult));
            #endregion
        }

    }
}
