using AuthServer.Infrastructure.Data.Identity;
using AuthService.Constants;
using AuthService.Extensions;
using AuthService.Infrastructure.Data.Context;
using AuthService.Infrastructure.Data.Identity;
using AuthService.Models;
using AuthService.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public RoleRepository(AppIdentityDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<bool> UpdateRole(List<string> roles, string userId)
        {
            try
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                var currentRoles = await _userManager.GetRolesAsync(currentUser);
                var rolesCurrent = await _userManager.RemoveFromRolesAsync(currentUser, currentRoles);
                var newRoles = _roleManager.Roles.Where(role => roles.Contains(role.Id)).Select(r => r.Name).ToArray();
                var addRoles = await _userManager.AddToRolesAsync(currentUser, newRoles);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<TreeRolesResponseModel> GetTreeRoles(string clientId)
        {
            try
            {
                //TreeRoleResponseViewModel treeRoleResponseViewModel = new TreeRoleResponseViewModel();

                // treeRolesResponseModel.Data <=> treeRoleResponseViewModel
                TreeRolesResponseModel treeRolesResponseModel = new TreeRolesResponseModel();
                treeRolesResponseModel.Data = new TreeRolesModel();

                // get all role by clientid
                var x = Convert.ToInt32(clientId);
                var roles = _roleManager.Roles.Where(role => role.ClientId == x).ToList();
                //treeRoleResponseViewModel.NodeRoles = new List<Models.NodeRole>();
                //treeRolesResponseModel.Data.NodeRoleData = new List<NodeRole>();

                foreach (var item in Enum.GetValues(typeof(RoleType)))
                {
                    //var fieldInfo = item.GetType().GetField(item.ToString());

                    //var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    //var a = descriptionAttributes[0].Description;

                    string title = EnumExtensionMethods.GetEnumDescription((RoleType)(item));

                    // level 1: RoleType
                    var children = new NodeRole()
                    {
                        Title = title,
                        Key = item.ToString(),
                        Value = item.ToString(),
                        //NodeRoleData = new List<NodeRole>()
                    };
                    // level 2: Module
                    // lấy danh sách module thuộc RoleType đó.
                    var listModuleofRoleType = _dbContext.Modules.Where(m => m.RoleType == item.ToString());
                    foreach (var module in listModuleofRoleType)
                    {
                        var childrenLower = new NodeRole()
                        {
                            Title = module.Description,
                            Key = module.Id.ToString(),
                            Value = module.Id.ToString(),
                            //NodeRoles = new List<NodeRole>()
                        };
                        // level 3: Role
                        var listRole = roles.Where(r => r.ModuleId == module.Id && r.RoleType == item.ToString());
                        foreach (var roleModule in listRole)
                        {
                            //get role
                            var childrenLower2 = new NodeRole()
                            {
                                Title = roleModule.Title,
                                Key = roleModule.Id.ToString(),
                                Value = roleModule.Id.ToString()
                            };
                            childrenLower.Children.Add(childrenLower2);
                        }
                        children.Children.Add(childrenLower);
                    }
                    treeRolesResponseModel.Data.Children.Add(children);
                }
                treeRolesResponseModel.Message = "Thành công";
                treeRolesResponseModel.Error = false;
                return treeRolesResponseModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
