using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Models
{
    public interface IModel
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool ResetId();
        DateTimeOffset CreationDate { get; set; }
        DateTimeOffset ModifiedDate { get; set; }

    }
}
