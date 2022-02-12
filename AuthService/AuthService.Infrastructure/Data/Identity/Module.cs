using AuthServer.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Data.Identity
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoleType { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
