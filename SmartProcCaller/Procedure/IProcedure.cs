using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SmartProcCaller.Procedure
{
    public interface IProcedure
    {
        DataRow Config { set; }

        DataTable GenerateTable();

        void ExecuteFromRow(DataRow row);
        void ExecuteFromTable(DataTable table);
    }
}
