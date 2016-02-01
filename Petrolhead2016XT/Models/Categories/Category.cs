using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Petrolhead2016XT.Models;
using Template10.Mvvm;

namespace Petrolhead2016XT.Models.Categories
{
    public abstract class Category : BindableBase, ICategory
    {
        private DateTimeOffset _creationDate = default(DateTimeOffset);
        public DateTimeOffset CreationDate
        {
            get
            {
                return _creationDate;
            }

            set
            {
                if (CreationDate != default(DateTimeOffset))
                    throw new InvalidOperationException("Creation date has already been set.");
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

        private string _Id = Guid.NewGuid().ToString();
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                Set(ref _Id, value);
            }
        }

        private DateTimeOffset _ModifiedDate = default(DateTimeOffset);
        public DateTimeOffset ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }

            set
            {
                Set(ref _ModifiedDate, value);
            }
        }

        private string _Name = default(string);
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                Set(ref _Name, value);
            }
        }

        public int CompareTo(string other)
        {
            if (Name == other)
                return 0;
            return 1;
        }

        public int CompareTo(ICategory other)
        {
            if (this == other)
                return 0;
            return 1;
        }

        public bool ResetId()
        {
            Id = Guid.NewGuid().ToString();
            return true;
        }
    }
}
