using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.Exceptions
{
    public class InvalidUserNameOrPassword:Exception
    {
        public InvalidUserNameOrPassword():base("Invalid UserName Or Password")
        {

        }
    }
}
