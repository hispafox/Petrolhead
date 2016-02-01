using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Models
{
    /// <summary>
    /// Models.IExpense interface
    /// Interface describing an expense
    /// </summary>
    public interface IExpense : IDataModel, IComparable<DateTimeOffset>, IComparable<IExpense>
    {
        DateTimeOffset TransactionDate { get; set; }
        string HumanTransactionDate { get; set; }
        List<IPart> Parts { get; set; }
        bool IsBudgetIncluded { get; set; }
        List<IPart> Search(ICategory category);
        List<IPart> Search(string name);
        List<IPart> Search(bool isInBudget);
        List<IPart> Search(long cost);
    }
}
