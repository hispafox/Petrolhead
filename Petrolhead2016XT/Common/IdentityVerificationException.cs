using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petrolhead2016XT.Common
{
    public class IdentityVerificationException : UnauthorizedAccessException
    {
        public IdentityVerificationException() : base("Identity verification failure")
        {

        }
        public IdentityVerificationException(string message) : base(message)
        {

        }

       
    }
}
