using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        #region About action method which returns the About.cshtml for /Home/About request
        public ActionResult About()
        {  
            string userName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[userName];
            if (authCookie == null)
            {
                ViewBag.loggedIn = 0;
            }
            else
            {
                ViewBag.loggedIn = 1;
            }   
            return View();   
            // For Unit Testing
            // return View("About");
        }
        #endregion
    }
}