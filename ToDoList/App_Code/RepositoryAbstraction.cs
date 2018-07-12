using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.App_Code
{
    public class RepositoryAbstraction
    {
        #region Repository Layer Interfaces
        IRepositoryDataAccount _iRepositoryDataAccount = null;
        IRepositoryDataAuthentication _iRepositoryDataAuthentication = null;
        #endregion

        #region Constructor for Injection
        public RepositoryAbstraction()
        {
            _iRepositoryDataAccount = GenericFactory<RepositorySQL, IRepositoryDataAccount>.CreateInstance();
            _iRepositoryDataAuthentication = GenericFactory<RepositorySQL, IRepositoryDataAuthentication>.CreateInstance();
        }
        #endregion

        #region To Do List Application Methods
        public string isValidUser(string userName, string password)
        {
            return _iRepositoryDataAuthentication.isValidUser(userName, password);
        }
        #endregion

        #region Get user TODOs List
        public List<ToDoModel> Details(string userName)
        {
            return _iRepositoryDataAccount.Details(userName);
        }
        #endregion

        #region Save and Get the new task created by the user
        public ToDoModel SaveNewTask(ToDoModel todoModel)
        {
            return _iRepositoryDataAccount.SaveNewTask(todoModel);
        }
        #endregion

        #region Marks the selected task as completed
        public int MarkAsCompleted(int ID, string userName)
        {
            return _iRepositoryDataAccount.MarkAsCompleted(ID, userName);
        }
        #endregion
    }
}