using AuthServer.Infrastructure.Data.Identity;
using AuthService.Constants;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Seeds
{
    //public static class DefaultUsers
    //{
    //    public static async Task SeedBasicUserAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
    //    {
    //        var defaultUser = new User
    //        {
    //            UserName = "basicuser@gmail.com",
    //            Email = "basicuser@gmail.com",
    //            Name = "basicuser@gmail.com",
    //            EmailConfirmed = true
    //        };
    //        if (userManager.Users.All(u => u.Id != defaultUser.Id))
    //        {
    //            var user = await userManager.FindByEmailAsync(defaultUser.Email);
    //            if (user == null)
    //            {
    //                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
    //                await userManager.AddToRoleAsync(defaultUser, Roles.Create.ToString());
    //            }
    //        }
    //    }
    //    public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    //    {
    //        var defaultUser = new User
    //        {
    //            UserName = "superadmin@gmail.com",
    //            Email = "superadmin@gmail.com",
    //            Name = "superadmin@gmail.com",
    //            EmailConfirmed = true
    //        };
    //        if (userManager.Users.All(u => u.Id != defaultUser.Id))
    //        {
    //            var user = await userManager.FindByEmailAsync(defaultUser.Email);
    //            if (user == null)
    //            {
    //                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
    //                await userManager.AddToRoleAsync(defaultUser, Roles.Create.ToString());
    //                await userManager.AddToRoleAsync(defaultUser, Roles.Confirm.ToString());
    //                await userManager.AddToRoleAsync(defaultUser, Roles.Review.ToString());
    //            }
    //            //await roleManager.SeedClaimsForSuperAdmin();
    //        }
    //    }
    //    private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
    //    {
    //        var adminRole = await roleManager.FindByNameAsync(Roles.Review.ToString());
    //        await roleManager.AddPermissionClaim(adminRole, "LinkBank", new List<string> { "Link", "UnLink" });
    //    }
    //    public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module, List<string> actions)
    //    {
    //        var allClaims = await roleManager.GetClaimsAsync(role);
    //        var allPermissions = Permissions.GeneratePermissionsForModule(module, actions);
    //        foreach (var permission in allPermissions)
    //        {
    //            if (!allClaims.Any(a => a.Type == "Permissions" && a.Value == permission))
    //            {
    //                await roleManager.AddClaimAsync(role, new Claim("Permissions", permission));
    //            }
    //        }
    //    }
    //}
}
