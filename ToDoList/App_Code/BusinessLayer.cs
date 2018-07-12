using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.App_Code
{
    public class BusinessLayer : IBusinessDataAuthentication, IBusinessDataAccount
    {
        #region Repository Abstraction Object reference
        RepositoryAbstraction repositoryAbstraction = new RepositoryAbstraction();
        #endregion

        #region IBusinessDataAuthentication method implementation
        // IsValidUser checks whether the user exists in the database
        public string IsValidUser(string userName, string password)
        {
            return repositoryAbstraction.isValidUser(userName, password);
        }
        #endregion

        #region TODO List Methods
        // Gets all the user tasks
        public List<ToDoModel> Details(string userName)
        {
            return repositoryAbstraction.Details(userName);
        }
        // Save and Get the new task created by the user
        public ToDoModel SaveNewTask(ToDoModel todoModel)
        {
            return repositoryAbstraction.SaveNewTask(todoModel);
        }

        // Marks the selected task as completed
        public int MarkAsCompleted(int ID, string userName)
        {
            return repositoryAbstraction.MarkAsCompleted(ID, userName);
        }
        #endregion

    }
}