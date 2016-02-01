using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Models
{
    
    public interface IVehicle : IDataModel, IComparable<DateTimeOffset>, IComparable<bool>
    {
        string CarMake { get; set; }
        string CarModel { get; set; }
        DateTimeOffset DateOfPurchase { get; set; }
        long Mileage { get; set; }
        
        DateTimeOffset NextWarrantDate { get; set; }
        DateTimeOffset LastWarrantDate { get; set; }
        DateTimeOffset NextRegoDate { get; set; }
        DateTimeOffset LastRegoDate { get; set; }
        bool IsBudgetEnabled { get; set; }
        bool IsOverBudget { get; set; }
        List<IExpense> Expenses { get; set; }

    
    }
}
