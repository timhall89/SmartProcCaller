using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Odbc;
using SmartProcCaller.DataFlows;
using System.Data;
using System.Diagnostics.Contracts;

namespace ExecuteDataFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            DataFlow dataFlow = new DataFlow();
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Key", typeof(byte)));
            table.Columns.Add(new DataColumn("SpecID", typeof(short)));
            table.Columns.Add(new DataColumn("ConnString", typeof(string)));
            table.Columns.Add(new DataColumn("CommandName", typeof(string)));
            table.Columns.Add(new DataColumn("DetailCommandName", typeof(string)));
            table.Columns.Add(new DataColumn("CommandIDParam", typeof(string)));

            DataRow newRow = table.NewRow();

            newRow["Key"] = 1;
            newRow["SpecID"] = 1;
            newRow["ConnString"] = "";
            newRow["CommandName"] = "dbo.GetDataFlowSpec";
            newRow["DetailCommandName"] = "dbo.GetDataSpecDetail";
            newRow["CommandIDParam"] = "DataFlowID";

            
            dataFlow.Execute(newRow);
         
        }
    }

    public static class Ext
    {
        //public static void MyExtension(this IEnumerable<KeyValuePair<object, object>>)
    }
}
