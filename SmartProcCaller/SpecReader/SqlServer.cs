using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProcCaller.Common;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SmartProcCaller.SpecReader
{
    class SqlServer : ISpecReader, IExterData
    {
        [ExternData("SpecID")]
        short id;

        [ExternData("ConnString")]
        string connString;

        [ExternData("CommandName")]
        string commandName;

        [ExternData("DetailCommandName")]
        string detailCommandName;

        [ExternData("CommandIDParam")]
        string commandIDParam;

        public DataRow Params
        {
            set { this.GetExternValsFromDataRow(value); }
        }
        public DataRow Spec
        {
            get
            {
                DataTable table = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand(commandName, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue(commandIDParam, id);

                    conn.Open();

                    table.Load(command.ExecuteReader());
                    if ((table?.Rows.Count ?? 0) == 0)
                        throw new InvalidOperationException("Initilization Failed");

                    return table.Rows[0];
                }
            }     
        }
        public DataTable SpecDetails
        {
            get
            {
                DataTable table = new DataTable();

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand command = new SqlCommand(detailCommandName, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue(commandIDParam, id);

                    conn.Open();
   
                    table.Load(command.ExecuteReader());

                    return table;
                }
            }
        }
    }
}
