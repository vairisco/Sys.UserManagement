using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.ResponseModels
{
    public class RegisterResModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public RegisterResModel(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
