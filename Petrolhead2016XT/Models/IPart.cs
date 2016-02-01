using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Models
{
    public interface IPart : IDataModel, IComparable<IPart>
    {
        bool IsBudgetIncluded { get; set; }
        
    }


}
