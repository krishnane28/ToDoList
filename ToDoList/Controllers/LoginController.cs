using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToDoList.App_Code;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class LoginController : Controller
    {
        #region Business Layer Reference
        private IBusinessDataAuthentication _iBusinessDataAuthentication;
        #endregion

        #region LoginController constructor 
        public LoginController()
        {
            // Reference to the Business Layer
            _iBusinessDataAuthentication = GenericFactory<BusinessLayer, IBusinessDataAuthentication>.CreateInstance();
        }

        public LoginController(IBusinessDataAuthentication businessDataAuthentication)
        {
            // Reference to the Business Layer
            _iBusinessDataAuthentication = businessDataAuthentication;
        }
        #endregion

        #region Action Methods
        // Gets the Login.cshtml for the /Login/Login request which is a get request
        public ActionResult Login()
        {
            // return View();
            // For Unit Testing
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Redirects the user to the details page if the log in is successful
        public ActionResult Login(LoginModel loginModel, string ReturnUrl)
        {
            if(ModelState.IsValid)
            {
                string userName = _iBusinessDataAuthentication.IsValidUser(loginModel.Username, loginModel.Password);
                if(userName == null)
                {
                    ViewBag.Message = "Invalid credentials";
                }
                else
                {
                    // FormsAuthentication.SetAuthCookie(userName, false);
                    if(ReturnUrl == "/" | ReturnUrl == null)
                    {
                        return RedirectToAction("Details", "ToDos");
                    }
                    else
                    {
                        return Redirect(ReturnUrl);
                    }
                }
            }
            return View();
            // For Unit Testing
            // return View("Login");
        }
        #endregion
    }
}