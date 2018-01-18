using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SmartProcCaller.Common
{
    public abstract class SqlConn
    {
        protected string server = string.Empty;
        protected string database = string.Empty;
        protected string username = string.Empty;
        protected string password = string.Empty;

        protected string ConnString
        {
            get
            {
                SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = server,
                    InitialCatalog = database
                };

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    stringBuilder.IntegratedSecurity = true;
                else
                {
                    stringBuilder.UserID = username;
                    stringBuilder.Password = password;
                }
                return stringBuilder.ConnectionString;
            }
        }
    }
}
