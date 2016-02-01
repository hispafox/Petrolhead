using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;
using Windows.Globalization.NumberFormatting;

namespace Petrolhead2016XT.Models
{
    public class Vehicle : Template10.Mvvm.BindableBase, IVehicle
    {
        
        private long _budgetTotal = default(long);
        /// <summary>
        /// Budget-specific total
        /// </summary>
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

        private string _carMake = default(string);
        /// <summary>
        /// Make of the vehicle
        /// </summary>
        public string CarMake
        {
            get
            {
                return _carMake;
            }

            set
            {
                Set(ref _carMake, value);
            }
        }

        private string _carModel = default(string);
        /// <summary>
        /// Model of the vehicle
        /// </summary>
        public string CarModel
        {
            get
            {
                return _carModel;
            }

            set
            {
                Set(ref _carModel, value);
            }
        }

        private ICategory _category = default(ICategory);
        /// <summary>
        /// Category of the vehicle.
        /// </summary>
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
        /// <summary>
        /// Total cost associated with a vehicle. 
        /// </summary>
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
        /// <summary>
        /// A vehicle's creation date.
        /// </summary>
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

        private DateTimeOffset _dateOfPurchase = default(DateTimeOffset);
        /// <summary>
        /// Date of a vehicle's purchase
        /// </summary>
        public DateTimeOffset DateOfPurchase
        {
            get
            {
                return _dateOfPurchase;
            }

            set
            {
                Set(ref _dateOfPurchase, value);
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

        private List<IExpense> _expenses = default(List<IExpense>);
        /// <summary>
        /// A list of expenses associated with a vehicle.
        /// </summary>
        public List<IExpense> Expenses
        {
            get
            {
                return _expenses;
            }

            set
            {
                Set(ref _expenses, value);
            }
        }

        private string _humanCost = default(string);
        /// <summary>
        /// Human-readable variant of a cost.
        /// </summary>
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
        /// <summary>
        /// GUID of a vehicle.
        /// </summary>
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
        /// <summary>
        /// Location of an image associated with a vehicle.
        /// </summary>
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

        private bool _isBudgetEnabled = default(bool);
        /// <summary>
        /// Toggles the status of budget management for a particular vehicle.
        /// </summary>
        public bool IsBudgetEnabled
        {
            get
            {
                return _isBudgetEnabled;
            }

            set
            {
                Set(ref _isBudgetEnabled, value);
            }
        }

        private bool _isOverBudget = default(bool);
        /// <summary>
        /// Is a vehicle over budget?
        /// </summary>
        public bool IsOverBudget
        {
            get
            {
                return _isOverBudget;
            }

            set
            {
                Set(ref _isOverBudget, value);
            }
        }

        private DateTimeOffset _lastRegoDate = default(DateTimeOffset);
        /// <summary>
        /// Date of a vehicle's last registration.
        /// </summary>
        public DateTimeOffset LastRegoDate
        {
            get
            {
                return _lastRegoDate;
            }

            set
            {
                Set(ref _lastRegoDate, value);
            }
        }

        private DateTimeOffset _lastWarrantDate = default(DateTimeOffset);
        /// <summary>
        /// Date of a vehicle's last warrant.
        /// </summary>
        public DateTimeOffset LastWarrantDate
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private long _mileage = default(long);
        /// <summary>
        /// The mileage associated with a vehicle.
        /// </summary>
        public long Mileage
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        private DateTimeOffset _modifiedDate = default(DateTimeOffset);
        /// <summary>
        /// The date a vehicle was last modified.
        /// </summary>
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
        /// <summary>
        /// The name associated with a specific vehicle. 
        /// </summary>
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

        private DateTimeOffset _nextRegoDate = default(DateTimeOffset);
        /// <summary>
        /// Date of a vehicle's next registration.
        /// </summary>
        public DateTimeOffset NextRegoDate
        {
            get
            {
                return _nextRegoDate;
            }

            set
            {
                Set(ref _nextRegoDate, value);
            }
        }

        private DateTimeOffset _nextWarrantDate = default(DateTimeOffset);
        /// <summary>
        /// Date of a vehicle's next warrant
        /// </summary>
        public DateTimeOffset NextWarrantDate
        {
            get
            {
                return _nextWarrantDate;
            }

            set
            {
                Set(ref _nextWarrantDate, value);
            }
        }


        /// <summary>
        /// Compares this vehicle.
        /// </summary>
        /// <param name="other">A DateTimeOffset object</param>
        /// <returns></returns>
        public int CompareTo(DateTimeOffset other)
        {
            if (this.NextWarrantDate == other)
                return 0;
            return 1;
        }

        public int CompareTo(bool other)
        {
            if (this.IsOverBudget == other)
                return 0;
            return 1;
        }

        public int CompareTo(ICategory other)
        {
            if (this.Category == other)
                return 0;
            return 1;
                
        }

        public int CompareTo(string other)
        {
            if (this.Name == other)
                return 0;
            return 1;
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
                BudgetTotal = 0;
                Cost = 0;
                foreach (var part in Expenses)
                {
                    Cost += part.Cost;
                    if (part.IsBudgetIncluded && this.IsBudgetEnabled)
                        BudgetTotal += part.Cost;
                }
                CurrencyFormatter currency = new CurrencyFormatter(Windows.System.UserProfile.GlobalizationPreferences.Currencies[0]);
                HumanCost = currency.Format(Cost);
                DateTimeFormatter datetime = new DateTimeFormatter("shortdate");
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
    }
}
