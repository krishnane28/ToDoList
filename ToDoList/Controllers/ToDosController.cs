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
    [Authorize]
    public class ToDosController : Controller
    {

        #region Business Layer Reference
        private IBusinessDataAccount _iBusinessDataAccount;
        #endregion

        #region ToDosController constructor 
        public ToDosController()
        {
            // Reference to the Business Layer
            _iBusinessDataAccount = GenericFactory<BusinessLayer, IBusinessDataAccount>.CreateInstance();
        }

        public ToDosController(IBusinessDataAccount businessDataAccount)
        {
            // Reference to the Business Layer
            _iBusinessDataAccount = businessDataAccount;
        }
        #endregion

        #region Authentication Ticket - Gets the logged in user cookie
        public string AuthTicket
        {
            get
            {
                string userName = FormsAuthentication.FormsCookieName;
                HttpCookie authCookie = HttpContext.Request.Cookies[userName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                return ticket.Name;                
            }
        }
        #endregion

        #region Action Methods
        // Gets all the TODO tasks of the User for the request /ToDos/Details
        public ActionResult Details()
        {
            // Returns the Details view with the current user's todo list
            return View(_iBusinessDataAccount.Details(AuthTicket));
            // For unit testing
            // return View(_iBusinessDataAccount.Details("Test1"));
        }

        // Create the Bootstrap Modal for creating new task
        public ActionResult CreateModal()
        {
            // Returns the _CreateTaskModal view which is a partial view
            return PartialView("_CreateTaskModal");
        }

        // Saves the newly added task by the user to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewTask(ToDoModel todoModel)
        {
            todoModel.Username = AuthTicket;
            // For Unit Testing   
            // todoModel.Username = "Test1";
            // Returns _SaveTask view which is a partial view along with the newly saved data
            return PartialView("_SaveTask", _iBusinessDataAccount.SaveNewTask(todoModel));
        }

        // Marks the selected task as completed
        [HttpPost]
        public ActionResult MarkAsCompleted(int ID)
        {
            int result = _iBusinessDataAccount.MarkAsCompleted(ID, AuthTicket);
            // For Unit Testing
            // int result = _iBusinessDataAccount.MarkAsCompleted(ID, "Test1");
            // Returns an empty result since this is just an update in the database
            return new EmptyResult();
        }

        // Performs the logout operation
        public ActionResult Logout()
        {
            // Removes the authetication ticket stored as cookie for the current user
            FormsAuthentication.SignOut();
            // Redirects to the About page 
            return RedirectToAction("About", "Home");
        }
        #endregion
    }
}