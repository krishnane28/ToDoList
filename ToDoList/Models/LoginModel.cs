using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    // Model for the table tblUsers
    public class LoginModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username cannot be blank")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password cannot be blank")]
        public string Password { get; set; }
    }
}