using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Reflection;
using SmartProcCaller.Common;

namespace SmartProcCaller.DataReader
{
    class FlatFile : IData_Reader, IExterData
    {
        [ExternData("FileName")]
        private string fileName = string.Empty;

        [ExternData("Deliminator")]
        private string deliminator = string.Empty;

        [ExternData("HasHeader")]
        private bool hasHeader = false;

        private string[] Headers;

        public DataRow Config { set { this.GetExternValsFromDataRow(value); } }

        private Details details = new Details();
        public Details Details { get { return details; } }

        public void FillTable(DataTable table)
        {
            if (details.Count == 0) return;

            string path = Path.GetDirectoryName(fileName);
            string name = Path.GetFileName(fileName);
            
            string[] files = Directory.GetFiles(path, name);

            foreach (string file in files) ReadAFileToTable(file, table);
        }

        private void ReadAFileToTable(string fName, DataTable table)
        {
            try
            {
                string line;
                StreamReader streamReader = new StreamReader(fName);
                while ((line = streamReader.ReadLine()) != null)
                    AddLineToTable(line, table);
            }
            catch { }
        }

        private void AddLineToTable(string line, DataTable table)
        {

            line = line.Replace("\0", "");
            if (string.IsNullOrEmpty(line)) return;
            try
            {
                string[] delimiter = new string[] { deliminator };
                string[] splitLine = line.Split(delimiter, StringSplitOptions.None);

                DataRow row = table.NewRow();

                foreach (Detail detail in details)
                {
                    try
                    {
                        switch (detail.Type)
                        {
                            case DetailType.Constant:
                                row[detail.MappedName] = detail.ValueIdentifier;
                                break;

                            case DetailType.ColumnNo:
                                if (int.TryParse(detail.ValueIdentifier, out int i))
                                {
                                    row[detail.MappedName] = splitLine[i - 1];
                                }
                                break;

                            case DetailType.ColumnName:
                                for (int n = 0; n <= Headers.GetUpperBound(0); n++)
                                {
                                    if (Headers[n] == detail.ValueIdentifier)
                                    {
                                        row[detail.MappedName] = splitLine[n];
                                    }
                                }
                                break;
                        }
                        table.Rows.Add(row);
                    }
                    catch { }
                }
            }
            catch { }
        }
    }
}
