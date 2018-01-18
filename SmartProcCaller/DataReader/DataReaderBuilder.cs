using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SmartProcCaller.DataReader
{
    public static class DataReaderBuilder
    {
        public static IData_Reader Build(DataRow Spec, DataTable SpecDetails)
        {
            byte key;
            try
            {
                key = (byte)Spec["DataSourceTypeID"];
            }
            catch
            {
                throw new ArgumentNullException("Could not find a key value.");
            }

            IData_Reader _DataReader = Build(key);

            _DataReader.Config = Spec;
            _DataReader.Details.Add(SpecDetails);

            return _DataReader;
        }

        private static IData_Reader Build(byte key)
        {
            switch (key)
            {
                case 1:
                    return new SqlServer();

                case 2:
                    return new ODBC();

                case 3:
                    return new FlatFile();

                case 5:
                    return new Constants();
                default:
                    throw new ArgumentOutOfRangeException("key",
                        string.Format("No data reader could be found corresponding to key: {0}", key));
            }
        }
    }

    
}
