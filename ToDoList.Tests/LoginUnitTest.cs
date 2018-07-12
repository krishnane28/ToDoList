using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using ToDoList.App_Code;
using ToDoList.Controllers;
using ToDoList.Models;

namespace ToDoList.Tests
{
    [TestClass]
    public class LoginUnitTest
    {
        [TestMethod]
        public void Unit_Test_Login()
        {
            #region Arrange Part
            Mock<IBusinessDataAuthentication> loginMock = new Mock<IBusinessDataAuthentication>();  
            /*
            LoginModel mockLoginModel = new LoginModel
            {
                Username = "test1",
                Password = "test100"
            };
            loginMock.Setup(login => login.IsValidUser(mockLoginModel.Username, mockLoginModel.Password)).Returns(mockLoginModel.Username);
            */
            LoginController testController = new LoginController(loginMock.Object);
            #endregion

            #region Act Part
            var actual = testController.Login() as ViewResult;
            #endregion

            #region Assert Part
            // Assert.IsInstanceOfType(actual, typeof(ViewResult));
            Assert.AreEqual("Login", actual.ViewName);
            #endregion
            
        }

        [TestMethod]
        public void Unit_Test_Post_Login()
        {
            #region Arrange Part
            Mock<IBusinessDataAuthentication> loginMock = new Mock<IBusinessDataAuthentication>();
            LoginModel mockLoginModel = new LoginModel
            {
                Username = "test1",
                Password = "test100"
            };
            loginMock.Setup(login => login.IsValidUser(mockLoginModel.Username, mockLoginModel.Password)).Returns(mockLoginModel.Username);
            LoginController testController = new LoginController(loginMock.Object);
            #endregion

            #region Act and Assert Parts
            if(mockLoginModel.Username == null | mockLoginModel.Password == null)
            {
                var actual = testController.Login(mockLoginModel, null) as ViewResult;
                Assert.AreEqual("Login", actual.ViewName);
            }
            else
            {
                var actual = testController.Login(mockLoginModel, null) as RedirectToRouteResult;
                Assert.AreEqual("Details", actual.RouteValues["action"]);
            }
            
            #endregion

        }
    }
}
