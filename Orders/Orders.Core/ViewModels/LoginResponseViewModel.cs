using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.ViewModels
{
    public class LoginResponseViewModel
    {
        public AccessTokenViewModel AccessToken { get; set; }
        public UserViewModel User { get; set; }
    }
}
