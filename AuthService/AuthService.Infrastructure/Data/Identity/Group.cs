using AuthServer.Infrastructure.Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Data.Identity
{
    public class Group
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ClientType { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
