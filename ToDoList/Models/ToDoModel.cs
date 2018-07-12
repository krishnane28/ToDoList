using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    // Model for the table tblToDos
    public class ToDoModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Task Name cannot be empty")]
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Username { get; set; }
        public bool IsDone { get; set; }
    }
}