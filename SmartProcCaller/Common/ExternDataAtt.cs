using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartProcCaller.Common
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExternDataAttribute : Attribute
    {
        private readonly string _ref;

        public ExternDataAttribute(string Ref = "")
        {
            _ref = Ref;
        }

        public string Ref { get { return _ref; } }
    }

    public interface IExterData { }

    public static class ObjectExtension
    {
        public static void GetExternValsFromDataRow(this IExterData o, DataRow row)
        {
            var bindingFlags = BindingFlags.Instance |
                   BindingFlags.NonPublic |
                   BindingFlags.Public;

            string varRef;

            foreach (var f in o.GetType().GetFields(bindingFlags))
            {
                ExternDataAttribute att = (ExternDataAttribute)f.GetCustomAttribute(typeof(ExternDataAttribute));

                if (att == null) continue;

                if (!string.IsNullOrEmpty(att.Ref))
                    varRef = att.Ref;
                else
                    varRef = f.Name;

                if (!row.Table.Columns.Contains(varRef))
                    throw new IndexOutOfRangeException(
                        string.Format("Value for {0} could not be found in the data row.", varRef));
                try
                {
                    f.SetValue(o, Convert.ChangeType(row[varRef], f.FieldType));
                }
                catch
                {
                    throw new InvalidCastException(
                        string.Format("Cannot convert value for varible {0}.", f.Name));
                }
            }
        }
    }
}
