using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Controllers;
using System.Web.Mvc;

namespace ToDoList.Tests
{
    [TestClass]
    public class HomeUnitTest
    {
        [TestMethod]
        // Unit test for About action method in HomeController
        public void Unit_Test_About()
        {
            #region Arrange Part
            HomeController testController = new HomeController();
            #endregion

            #region Act Part
            var actual = testController.About() as ViewResult;
            #endregion

            #region Assert Part
            Assert.AreEqual("About", actual.ViewName);
            #endregion
        }
    }
}
