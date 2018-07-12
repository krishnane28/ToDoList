using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.App_Code
{
    public class DataAbstraction
    {
        #region Data Access Layer interface 
        private IDataAccess _iDataAccess = null;
        #endregion

        #region Constructors for injection 
        public DataAbstraction() : this(new DataAccessSQL())
        {

        }
        public DataAbstraction(IDataAccess iDataAccess)
        {
            _iDataAccess = iDataAccess;
        }
        #endregion

        #region Get Single Data
        public object GetSingleData(string sql, List<DbParameter> parameterList)
        {
            try
            {
                return _iDataAccess.GetSingleData(sql, parameterList);
            }
            catch
            {
                throw null;
            }
        }
        #endregion

        #region Get Multiple Data
        public List<ToDoModel> GetMultipleData(string sql, List<DbParameter> parameterList)
        {
            try
            {
                return _iDataAccess.GetMultipleData(sql, parameterList);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Insert or Update or Delete
        public int InsOrUpdOrDel(string sql, List<DbParameter> parameterList)
        {
            try
            {
                return _iDataAccess.InsOrUpdOrDel(sql, parameterList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Using Stored Procedures 
        public ToDoModel UsingSP(string storedProcedure, List<DbParameter> parameterList)
        {
            try
            {
                return _iDataAccess.UsingSP(storedProcedure, parameterList);
            }
            catch 
            {
                return null;
            }
        }
        #endregion
    }
}