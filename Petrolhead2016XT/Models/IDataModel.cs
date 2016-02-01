using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Models
{
    public interface IDataModel : IModel, IComparable<string>, IComparable<ICategory>
    {
        Task<bool> UpdateData();
        long Cost { get; set; }
        string HumanCost { get; set; }
        long BudgetTotal { get; set; }
        ICategory Category { get; set; }
        Uri ImageLocation { get; set; }
    }
}
