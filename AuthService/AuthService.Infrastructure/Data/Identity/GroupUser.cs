using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Data.Identity
{
    public class GroupUser
    {
        public string UserId { get; set; }
        public string GroupId { get; set; }
    }
}
