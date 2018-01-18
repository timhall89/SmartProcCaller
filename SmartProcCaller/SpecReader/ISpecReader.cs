using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SmartProcCaller.SpecReader
{
    public interface ISpecReader
    {
        DataRow Params { set; }

        DataRow Spec { get; }

        DataTable SpecDetails { get; }
    }
}
