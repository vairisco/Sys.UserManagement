using AuthService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<bool> UpdateRole(List<string> roles, string userId);
        Task<TreeRolesResponseModel> GetTreeRoles(string clientId);
    }
}
