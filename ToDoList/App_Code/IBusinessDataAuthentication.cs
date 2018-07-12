using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.App_Code
{
    
    public interface IBusinessDataAuthentication
    {
        #region IsValidUser checks whether the user exists in the database
        string IsValidUser(string uname, string pwd);
        #endregion
    }

}
