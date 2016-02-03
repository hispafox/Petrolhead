using System;
using Petrolhead2016XT.Models;
using System.Collections.Generic;

namespace Petrolhead2016XT.Repositories
{
    public interface IRepository<TDataModel> 
        where TDataModel: IDataModel
    {
        TDataModel Clone(TDataModel part);
        TDataModel Factory(string name, string description = null, ICategory category = null, bool? isBudget = default(bool?), Uri imgLocation = null);
    }

    public interface IExpenseRepository<TExpense>
        where TExpense : IExpense
    {
        TExpense Clone(TExpense expense);
        TExpense Factory(string name, string description = null, ICategory category = null, bool? isBudget = default(bool?), Uri imgLocation = null, List<IPart> parts = null);
    }
}