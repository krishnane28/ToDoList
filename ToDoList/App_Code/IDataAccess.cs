using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.App_Code
{
    public interface IDataAccess
    {
        #region Methods for communicating with the data source
        object GetSingleData(string sql, List<DbParameter> parameterList);
        List<ToDoModel> GetMultipleData(string sql, List<DbParameter> parameterList);
        int InsOrUpdOrDel(string sql, List<DbParameter> parameterList);
        ToDoModel UsingSP(string storedProcedure, List<DbParameter> parameterList);
        #endregion
    }
}
