using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.ViewModels
{
    public class ResponseAPIViewModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseAPIViewModel(bool status,string message,object data)
        {
            Message = message;
            Status = status;
            Data = data;
        }
    }
}
