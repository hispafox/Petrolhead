using Petrolhead2016XT.Models;
using Petrolhead2016XT.Models.Categories;
using Petrolhead2016XT.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Repositories
{
    public class PartRepository : IRepository<Part>
    {
        public Models.Part Factory(string name, string description = null, ICategory category = null, bool? isBudget = null, Uri imgLocation = null)
        {
            return new Models.Part()
            {
                Name = name,
                Description = description ?? "",
                Category = category ?? new Category() { Name = "Test", CreationDate = DateTime.Today, ModifiedDate = DateTime.Now},
                CreationDate = DateTime.Today,
                ModifiedDate = DateTime.Now,
                ImageLocation = imgLocation ?? new Uri("ms-appx:///Assets/StoreLogo.png")
                          };
        }
        public Models.Part Clone(Part part)
        {
            return Factory(part.Name, part.Description, part.Category, part.IsBudgetIncluded, part.ImageLocation);
        }
    }

    public class ExpenseRepository : IExpenseRepository<Expense>
    {
        public Expense Clone(Expense expense)
        {
            return Factory(expense.Name, expense.Description, expense.Category, expense.IsBudgetIncluded, expense.ImageLocation, expense.Parts);
        }

        public Expense Factory(string name, string description = null, ICategory category = null, bool? isBudget = default(bool?), Uri imgLocation = null, List<IPart> parts = null)
        {
            return new Expense()
            {
                Name = name,
                Description = description ?? "",
                Category = category ?? new Category() { Name = "Test" },
                IsBudgetIncluded = isBudget ?? false,
                ImageLocation = imgLocation ?? new Uri("ms-appx:///Assets/StoreLogo.png"),
                Parts = parts ?? new List<IPart>(),
                
            };
        
        }
    }
}
