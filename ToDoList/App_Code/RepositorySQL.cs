using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.App_Code
{
    public class RepositorySQL : IRepositoryDataAuthentication, IRepositoryDataAccount
    {
        #region Data Abstraction reference
        private DataAbstraction _dataAbstraction = null;
        #endregion

        #region Constructor for injection 
        public RepositorySQL() : this(new DataAbstraction())
        {

        }
        public RepositorySQL(DataAbstraction dataAbstraction)
        {
            _dataAbstraction = dataAbstraction;
        }
        #endregion

        #region IRepositoryDataAuthentication Methods implementation
        // User Authentication method
        public string isValidUser(string userName, string password)
        {
            string result = null;
            
            try
            {
                string sql = "select Username from dbo.tblUsers where Username=@uname and Password=@pwd";
                List<DbParameter> parameterList = new List<DbParameter>();
                SqlParameter parameterUsername = new SqlParameter("@uname", SqlDbType.NVarChar, 50);
                parameterUsername.Value = userName;
                parameterList.Add(parameterUsername);
                SqlParameter parameterPassword = new SqlParameter("@pwd", SqlDbType.NVarChar, 50);
                parameterPassword.Value = password;
                parameterList.Add(parameterPassword);
                object userObject = _dataAbstraction.GetSingleData(sql, parameterList);
                if (userObject != null)
                {
                    result = userObject.ToString();
                }
            }
            catch
            {
                result = null;
            }            
            return result;
        }
        #endregion

        #region IRepositoryDataAccount Methods implementation
        // Gets the user task list
        public List<ToDoModel> Details(string userName)
        {
            try
            {
                string sql = "SELECT * FROM dbo.tblToDos WHERE Username=@uname";
                List<DbParameter> parameterList = new List<DbParameter>();
                SqlParameter parameterUsername = new SqlParameter("@uname", SqlDbType.NVarChar, 50);
                parameterUsername.Value = userName;
                parameterList.Add(parameterUsername);
                return _dataAbstraction.GetMultipleData(sql, parameterList);
            }
            catch
            {
                return null;
            }
            
        }
        // Save and Get the new task created by the user
        public ToDoModel SaveNewTask(ToDoModel todoModel)
        {
            string storedProcedure = "dbo.spSaveNewTask";
            List<DbParameter> parameterList = new List<DbParameter>();
            SqlParameter parameterTaskName = new SqlParameter("@taskName", SqlDbType.NVarChar, 50);
            parameterTaskName.Value = todoModel.TaskName;
            parameterList.Add(parameterTaskName);
            SqlParameter parameterTaskDescription = new SqlParameter("@taskDescription", SqlDbType.NVarChar, 500);
            parameterTaskDescription.Value = todoModel.TaskDescription;
            parameterList.Add(parameterTaskDescription);
            SqlParameter parameterUsername = new SqlParameter("@username", SqlDbType.NVarChar, 50);
            parameterUsername.Value = todoModel.Username;
            parameterList.Add(parameterUsername);
            return _dataAbstraction.UsingSP(storedProcedure, parameterList);
        }

        // Marks the selected task as completed
        public int MarkAsCompleted(int ID, string userName)
        {
            string sql = "UPDATE dbo.tblToDos SET IsDone = 1 WHERE ID=@id and Username=@username";
            List<DbParameter> parameterList = new List<DbParameter>();
            SqlParameter parameterID = new SqlParameter("@id", SqlDbType.Int);
            parameterID.Value = ID;
            parameterList.Add(parameterID);
            SqlParameter parameterUsername = new SqlParameter("@username", SqlDbType.NVarChar, 50);
            parameterUsername.Value = userName;
            parameterList.Add(parameterUsername);
            return _dataAbstraction.InsOrUpdOrDel(sql, parameterList);
        }
        #endregion

    }
}