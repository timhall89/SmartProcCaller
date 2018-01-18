using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SmartProcCaller.DataReader
{
    class Constants : IData_Reader
    {
        private Details details = new Details();
        public Details Details { get { return details; } }

        public DataRow Config { set { } }

        public void FillTable(DataTable table)
        {
            if (details.Count == 0) return;

            DataRow row = table.NewRow();

            foreach (Detail detail in details)
            {
                if (detail.Type == DetailType.Constant)
                {
                    try { row[detail.MappedName] = detail.ValueIdentifier; } catch { }
                }
            }
            table.Rows.Add(row);
        }
    }
}
