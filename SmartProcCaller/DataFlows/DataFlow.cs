using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartProcCaller.DataReader;
using SmartProcCaller.Procedure;
using SmartProcCaller.SpecReader;
using System.Data;

namespace SmartProcCaller.DataFlows
{
    public class DataFlow
    {
        private ISpecReader specReader;
        private IData_Reader dataReader;
        private IProcedure procedure;

        public void Execute(DataRow Params)
        {
            specReader = SpecReaderBuilder.Build(Params);

            DataRow Spec = specReader.Spec;

            dataReader = DataReaderBuilder.Build(Spec, specReader.SpecDetails);

            procedure = ProcedureBuilder.Build(Spec);

            DataTable data = procedure.GenerateTable();

            dataReader.FillTable(data);

            procedure.ExecuteFromTable(data); 

        }
    }
}
