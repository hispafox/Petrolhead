using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Windows.Globalization.DateTimeFormatting;
using Windows.Globalization.NumberFormatting;
using Windows.UI.Popups;

namespace Petrolhead2016XT.Models
{
    /// <summary>
    /// Models.Part class
    /// Inherits from the Template10.Mvvm.BindableBase class.
    /// Provides an implementation of the IPart interface.
    /// Acts as a container for one expense.
    /// </summary>
    public class Part : BindableBase, IPart
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

        private string _id = Guid.NewGuid().ToString();
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

        private bool _isBudgetIncluded = default(bool);
        public bool IsBudgetIncluded
        {
            get
            {
                return _isBudgetIncluded;
            }

            set
            {
                Set(ref _isBudgetIncluded, value);
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

        public int CompareTo(ICategory other)
        {
            if (this.Category.CompareTo(other) == 0)
                return 0;
            return 1;
        }

        public int CompareTo(string other)
        {
            if (other == this.Name)
            {
                return 0;
            }
            else
            {

                return 1;
              
            }
               

        }

        public bool ResetId()
        {
            Id = Guid.NewGuid().ToString();
            return true;
        }

        public async Task<bool> UpdateData()
        {
            try
            {
                while (App.Busy)
                {
                    await Task.Delay(100);
                }
                App.Busy = true;
                CurrencyFormatter currency = new CurrencyFormatter(Windows.System.UserProfile.GlobalizationPreferences.Currencies[0]);
                HumanCost = currency.Format(Cost);
                ModifiedDate = DateTime.Now;
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

        public int CompareTo(IPart other)
        {
            if ((IPart)this == other)
                return 0;
            return 1;
        }
    }
}
