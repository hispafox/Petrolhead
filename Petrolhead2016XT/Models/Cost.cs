using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;
using Windows.Globalization.NumberFormatting;

namespace Petrolhead2016XT.Models
{
    /// <summary>
    /// Models.Expense class
    /// Provides an implementation of IExpense
    /// Provides transaction-related data; can contain multiple IPart objects.
    /// </summary>
    public class Expense : Template10.Mvvm.BindableBase, IExpense
    {
        private long _budgetTotal = default(long);
        public long BudgetTotal
        {
            get
            {
                return _budgetTotal;
            }

            set
            {
                Set(ref _budgetTotal, value);
            }
        }

        private ICategory _category = default(ICategory);
        public ICategory Category
        {
            get
            {
                return _category;
            }

            set
            {
                Set(ref _category, value);
            }
        }

        private long _cost = default(long);
	
        public long Cost
        {
            get
            {
                return _cost;
            }

            set
            {
                Set(ref _cost, value);
            }
        }

        private DateTimeOffset _creationDate = default(DateTimeOffset);
        public DateTimeOffset CreationDate
        {
            get
            {
                return _creationDate;
            }

            set
            {
                Set(ref _creationDate, value);
            }
        }

        private string _description = default(string);
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                Set(ref _description, value);
            }
        }

        private string _humanCost = default(string);
        public string HumanCost
        {
            get
            {
                return _humanCost;
            }

            set
            {
                Set(ref _humanCost, value);
            }
        }

        private string _humanTransactionDate = default(string);
        public string HumanTransactionDate
        {
            get
            {
                return _humanTransactionDate;
            }

            set
            {
                Set(ref _humanTransactionDate, value);
            }
        }

        private string _id = default(string);
        public string Id
        {
            get
            {
                return _id;
            }

            set
            {
                Set(ref _id, value);
            }
        }

        private Uri _imageLocation = default(Uri);
        public Uri ImageLocation
        {
            get
            {
                return _imageLocation;
            }

            set
            {
                Set(ref _imageLocation, value);
            }
        }

        private bool _isIncludedInBudget = default(bool);
        public bool IsBudgetIncluded
        {
            get
            {
                return _isIncludedInBudget;
            }

            set
            {
                Set(ref _isIncludedInBudget, value);
            }
        }

        private DateTimeOffset _modifiedDate = default(DateTimeOffset);
        public DateTimeOffset ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }

            set
            {
                Set(ref _modifiedDate, value);
            }
        }

        private string _name = default(string);
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                Set(ref _name, value);
            }
        }

        private List<IPart> _Parts = default(List<IPart>);
        public List<IPart> Parts
        {
            get
            {
                return _Parts;
            }

            set
            {
                Set(ref _Parts, value);
            }
        }

        private DateTimeOffset _transactionDate = default(DateTimeOffset);
        public DateTimeOffset TransactionDate
        {
            get
            {
                return _transactionDate;
            }

            set
            {
                Set(ref _transactionDate, value);
            }
        }

        public int CompareTo(DateTimeOffset other)
        {
            if (TransactionDate.Equals(other))
                return 0;
            return 1;
        }

        public int CompareTo(ICategory other)
        {
            if (Category.Equals(other))
                return 0;
            return 1;
        }

        public int CompareTo(string other)
        {
            if (Name.Equals(other))
                return 0;
            return 1;
        }

        public bool ResetId()
        {
            Id = Guid.NewGuid().ToString();
            return true;
        }

        public List<IPart> Search(bool isInBudget)
        {
            List<IPart> parts = new List<IPart>();
            foreach (var part in Parts)
            {
                if (part.IsBudgetIncluded == isInBudget)
                {
                    parts.Add(part);
                }
            }
            return parts;
        }

        public List<IPart> Search(long cost)
        {
            List<IPart> parts = new List<IPart>();
            foreach (var part in Parts)
            {
                if (part.Cost == cost)
                    parts.Add(part);
            }
            return parts;
        }

        public List<IPart> Search(string name)
        {
            List<IPart> parts = new List<IPart>();

            foreach (var part in Parts)
            {
                if (part.Name == name)
                    parts.Add(part);
            }
            return parts;
        }

        public List<IPart> Search(ICategory category)
        {
            List<IPart> parts = new List<IPart>();

            foreach (var part in Parts)
            {
                if (part.Category == category)
                    parts.Add(part);
            }
            return parts;
        }

        /// <summary>
        /// Expense.UpdateData() method
        /// Updates all data
        /// </summary>
        /// <returns>
        /// Returns a bool object.
        /// </returns>
        public async Task<bool> UpdateData()
        {
            try
            {
                while (App.Busy)
                {
                    await Task.Delay(100);
                }
                App.Busy = true;
                BudgetTotal = 0;
                Cost = 0;
                foreach (var part in Parts)
                {
                    Cost += part.Cost;
                    if (part.IsBudgetIncluded && this.IsBudgetIncluded)
                        BudgetTotal += part.Cost;
                }
                CurrencyFormatter currency = new CurrencyFormatter(Windows.System.UserProfile.GlobalizationPreferences.Currencies[0]);
                HumanCost = currency.Format(Cost);
                DateTimeFormatter datetime = new DateTimeFormatter("shortdate");
                HumanTransactionDate = datetime.Format(TransactionDate);
                return true;

            }
            catch (Exception ex)
            {
                App.Telemetry.TrackException(ex);
                return false;
            }
            finally
            {
                App.Busy = false;
            }
            
        }

        public int CompareTo(IExpense other)
        {
            if ((IExpense)this == other)
                return 0;
            return 1;
        }
    }
}
