using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SmartProcCaller.Common;
using System.Reflection;

namespace SmartProcCaller.Procedure
{
    class SqlProcedure : IProcedure, IExterData
    {
        private DataTable table = new DataTable();
        private SqlCommand command = new SqlCommand();

        [ExternData("StoredProcName")]
        private string CmdText;
        
        [ExternData("DestConnString")]
        private string connString;
  
        public DataRow Config
        {
            set
            {
                this.GetExternValsFromDataRow(value);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = CmdText;
                DeriveParams();
            }
        }

        private void DeriveParams()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    command.Connection = conn;
                    conn.Open();
                    SqlCommandBuilder.DeriveParameters(command);
            }
                catch { }
        }
        }

        public DataTable GenerateTable()
        {
    
            DataTable table = new DataTable();
            string col = string.Empty;
            foreach (SqlParameter param in command.Parameters)
            {
                if (param.Direction != ParameterDirection.ReturnValue)
                {
                    try
                    {
                        col = param.ParameterName.Substring(1, param.ParameterName.Length - 1);
                        DataColumn column = new DataColumn(col, ToClrType(param.SqlDbType));
                        if (param.Size > 0) column.MaxLength = param.Size;
                        table.Columns.Add(column);
                    }
                    catch { }
                }
            }
            return table;
        }

        public void ExecuteFromTable(DataTable table)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                command.Connection = conn;
                conn.Open();
                foreach (DataRow row in table.Rows) command.ExecFromDataRow(row);
            }
        }

        public void ExecuteFromRow(DataRow row)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                command.Connection = conn;
                conn.Open();
                command.ExecFromDataRow(row);
            }
        }

        private Type ToClrType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long);

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal);

                case SqlDbType.Float:
                    return typeof(double);

                case SqlDbType.Int:
                    return typeof(int);

                case SqlDbType.Real:
                    return typeof(float);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid);

                case SqlDbType.SmallInt:
                    return typeof(short);

                case SqlDbType.TinyInt:
                    return typeof(byte);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }
    }

    static class SQLExtensions
    {
        public static void ExecFromDataRow(this SqlCommand command, DataRow row)
        {
            command.SetParamsFromDataRow(row);
            command.ExecuteNonQuery();
            command.ReadOutParamsToDataRow(row);
        }

        public static void SetParamsFromDataRow(this SqlCommand command, DataRow data)
        {
            string col = string.Empty;
            foreach (SqlParameter param in command.Parameters)
            {
                if (param.Direction == ParameterDirection.Input || param.Direction == ParameterDirection.InputOutput)
                {
                    try
                    {
                        col = param.ParameterName.Substring(1, param.ParameterName.Length - 1);
                        param.Value = data[col];
                    }
                    catch { param.Value = null; }
                }
            }
        }

        public static void ReadOutParamsToDataRow(this SqlCommand command, DataRow data)
        {
            string col = string.Empty;
            foreach (SqlParameter param in command.Parameters)
            {
                if (param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.Output)
                {
                    try
                    {
                        col = param.ParameterName.Substring(1, param.ParameterName.Length - 1);
                        data[col] = param.Value;
                    }
                    catch { }
                }
            }
        }

    }
}


