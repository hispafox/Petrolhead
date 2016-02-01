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

        public string Id
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

        public int CompareTo(string other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ICategory other)
        {
            throw new NotImplementedException();
        }

        public bool ResetId()
        {
            throw new NotImplementedException();
        }
    }
}
