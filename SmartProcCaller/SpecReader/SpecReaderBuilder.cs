using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SmartProcCaller.SpecReader
{
    public static class SpecReaderBuilder
    {
        public static ISpecReader Build(DataRow Params)
        {
            byte key;
            try
            {
                key = (byte)Params["Key"];
            }
            catch
            {
                throw new ArgumentNullException("Could not find a key value.");
            }

            ISpecReader specReader = Build(key);

            specReader.Params = Params;

            return specReader;
        }

        public static ISpecReader Build(byte key)
        {
            switch (key)
            {
                case 1:
                    return new SqlServer();

                default:
                    throw new ArgumentOutOfRangeException("key",
                        string.Format("No Spec Reader could be found for key: {0}", key));
            }
        }
    }
}
