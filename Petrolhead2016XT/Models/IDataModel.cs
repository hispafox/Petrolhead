using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Models
{
    /// <summary>
    /// Provides a base for all data model classes and interfaces.
    /// </summary>
    public interface IDataModel : IModel, IComparable<string>, IComparable<ICategory>
    {
        /// <summary>
        /// Signature for a method which updates data for a model, usually on the PropertyChanged event.
        /// </summary>
        /// <returns>Returns a bool object</returns>
        Task<bool> UpdateData();
        /// <summary>
        /// Represents the cost associated with a model.
        /// </summary>
        long Cost { get; set; }
        /// <summary>
        /// A string representation of Cost.
        /// </summary>
        string HumanCost { get; set; }
        /// <summary>
        /// A budget-friendly total.
        /// </summary>
        long BudgetTotal { get; set; }
        /// <summary>
        /// Represents the category of a model.
        /// </summary>
        ICategory Category { get; set; }
        /// <summary>
        /// Location of an image associated with a model.
        /// </summary>
        Uri ImageLocation { get; set; }
    }
}
