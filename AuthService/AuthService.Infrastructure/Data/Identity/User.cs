using AuthService.Infrastructure.Common;
using AuthService.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Infrastructure.Data.Identity
{
    public class User : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
