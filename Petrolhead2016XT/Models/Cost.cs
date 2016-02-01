using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Models
{
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
                throw new NotImplementedException();
            }
        }

        public long Cost
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

        public DateTimeOffset CreationDate
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

        public string Description
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

        public string HumanCost
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

        public string HumanTransactionDate
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

        public Uri ImageLocation
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

        public bool IsBudgetIncluded
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

        public DateTimeOffset ModifiedDate
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

        public string Name
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

        public List<IPart> Parts
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

        public DateTimeOffset TransactionDate
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

        public int CompareTo(DateTimeOffset other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ICategory other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(string other)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        public bool UpdateData()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IExpense other)
        {
            if ((IExpense)this == other)
                return 0;
            return 1;
        }
    }
}
