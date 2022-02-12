using AuthService.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace AuthServer.Infrastructure.Data.Identity
{
    public class Role : IdentityRole
    {
        public Role() : base()
        {

        }
        // Tạo lập - Init, kiểm soát - Review, duyệt - Approve
        public string RoleType { get; set; }
        public string Title { get; set; }
        public int ModuleId { get; set; }
        public int ClientId { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
