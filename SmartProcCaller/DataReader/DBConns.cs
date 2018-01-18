using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using SmartProcCaller.Common;
using System.Reflection;

namespace SmartProcCaller.DataReader
{
    class SqlServer : DBConnSpec, IData_Reader
    {
        public void FillTable(DataTable table)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                try
                {
                    conn.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    table.Load(dataReader);
                }
                catch { }
            }
        }
    }

    class ODBC : DBConnSpec, IData_Reader
    {
        public void FillTable(DataTable table)
        {
            using (OdbcConnection conn = new OdbcConnection(connString))
            using (OdbcCommand command = new OdbcCommand(sql, conn))
            {
                try
                {
                    conn.Open();
                    OdbcDataReader dataReader = command.ExecuteReader();
                    table.Load(dataReader);
                }
                catch { }
            }
        }
    }

    abstract class DBConnSpec : IExterData
    {
        private Details details = new Details();
        public Details Details { get { return details; } }

        [ExternData("ConnString")]
        protected string connString = string.Empty;

        [ExternData("SQL")]
        protected string sql = string.Empty;

        public DataRow Config { set { this.GetExternValsFromDataRow(value); } }
    }
}
