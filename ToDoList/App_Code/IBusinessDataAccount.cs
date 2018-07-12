using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.App_Code
{
    public interface IBusinessDataAccount
    {
        #region Get User TODOs List
        List<ToDoModel> Details(string userName);
        #endregion

        #region Save and Get the new task created by the user
        ToDoModel SaveNewTask(ToDoModel todoModel);
        #endregion

        #region Marks the selected task as completed
        int MarkAsCompleted(int ID, string userName);
        #endregion
    }
}
