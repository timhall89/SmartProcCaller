using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SmartProcCaller.Procedure
{
    public static class ProcedureBuilder
    {
        public static IProcedure Build(DataRow Spec)
        {
            byte key;
            try
            {
                key = (byte)Spec["ProcTypeKey"];
            }
            catch
            {
                throw new ArgumentNullException("Could not find a key value.");
            }

            IProcedure procedure = Build(key);

            procedure.Config = Spec;

            return procedure;
        }
        public static IProcedure Build(byte key)
        {
            switch (key)
            {
                case 1:
                    return new SqlProcedure();
                   
                default:
                    throw new ArgumentOutOfRangeException("key",
                        string.Format("No proceedure could be found for key: {0}", key));
            }
        }
    }
}
