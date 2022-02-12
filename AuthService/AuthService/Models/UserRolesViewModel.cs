using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
