using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.App_Code
{
    public class DataAccessSQL : IDataAccess
    {
        #region Connection String for SQL Server
        private string ConnectionString = ConfigurationManager.ConnectionStrings["TODOList"].ConnectionString;
        #endregion

        #region Get Single Data
        public object GetSingleData(string sql, List<DbParameter> parameterList)
        {           
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                object userObject = null;
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                foreach (SqlParameter sqlParameterList in parameterList)
                {
                    command.Parameters.Add(sqlParameterList);
                }
                userObject = command.ExecuteScalar();
                return userObject;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Get Multiple Data
        public List<ToDoModel> GetMultipleData(string sql, List<DbParameter> parameterList)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                List<ToDoModel> userTasks = new List<ToDoModel>();
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(sql, connection);
                foreach (SqlParameter sqlParameterList in parameterList)
                {
                    command.Parameters.Add(sqlParameterList);
                }
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataTable);
                foreach(DataRow row in dataTable.Rows)
                {
                    userTasks.Add(new ToDoModel { ID = Convert.ToInt32(row["ID"]),
                                                  TaskName = Convert.ToString(row["TaskName"]),
                                                  TaskDescription = Convert.ToString(row["TaskDescription"]),
                                                  Username = Convert.ToString(row["Username"]),
                                                  IsDone = true ? Convert.ToInt32(row["IsDone"]) == 1 : false});
                }
                if(userTasks.Count() == 0)
                {
                    return null;
                }
                else
                {
                    return userTasks;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Insert or Update or Delete
        public int InsOrUpdOrDel(string sql, List<DbParameter> parameterList)
        {
            int rowsModified = 0;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                foreach (SqlParameter sqlParameterList in parameterList)
                {
                    command.Parameters.Add(sqlParameterList);
                }
                rowsModified = command.ExecuteNonQuery();
                return rowsModified;
            }
            catch
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
            
        }
        #endregion

        #region Using Stored Procedures
        public ToDoModel UsingSP(string storedProcedure, List<DbParameter> parameterList)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            ToDoModel newToDoModel = new ToDoModel();
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand(storedProcedure, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParameter in parameterList)
                {
                    command.Parameters.Add(sqlParameter);
                }
                dataAdapter.SelectCommand = command;
                dataAdapter.Fill(dataTable);
                DataRow newRow = dataTable.Rows[0];
                newToDoModel.ID = Convert.ToInt32(newRow["ID"]);
                newToDoModel.TaskName = Convert.ToString(newRow["TaskName"]);
                newToDoModel.TaskDescription = Convert.ToString(newRow["TaskDescription"]);
                newToDoModel.Username = Convert.ToString(newRow["Username"]);
                newToDoModel.IsDone = false;
                return newToDoModel;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}