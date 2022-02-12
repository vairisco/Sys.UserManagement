using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Models
{
    public class AddActionViewModel
    {
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public string Description { get; set; }
        public string RoleId { get; set; }
    }
}
