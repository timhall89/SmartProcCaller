using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using SmartProcCaller.Common;

namespace SmartProcCaller.DataReader
{
    public interface IData_Reader 
    {
        DataRow Config { set; }

        Details Details { get; }

        void FillTable(DataTable table);
    }

    public class Details : IEnumerable<Detail>
    {
        public IEnumerator<Detail> GetEnumerator()
        {
            return details.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return details.GetEnumerator();
        }

        private List<Detail> details = new List<Detail>();

        public int Count { get { return details.Count; } }

        public void Add(Detail detail)
        {
            details.Add(detail);
        }

        public void Add(DataTable table)
        {
            foreach (DataRow row in table.Rows) Add(new Detail(row));
        }
    }

    public class Detail : IExterData
    {
        public Detail() { }

        public Detail(DataRow row)
        {
            this.GetExternValsFromDataRow(row);
        }

        [ExternDataAttribute("TypeID")]
        public DetailType Type;

        [ExternDataAttribute()]
        public string ValueIdentifier;

        [ExternDataAttribute()]
        public string MappedName;
    }

    public enum DetailType
    {
        Constant = 1,
        ColumnNo = 2,
        ColumnName = 3,
        XmlElement = 4
    }
}