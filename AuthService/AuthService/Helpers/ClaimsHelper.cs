using AuthServer.Infrastructure.Data.Identity;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthService.Helpers
{
    //public static class ClaimsHelper
    //{
    //    public static void GetPermissions(this List<RoleClaimsViewModel> allPermissions, Type policy, string roleId)
    //    {
    //        FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);
    //        foreach (FieldInfo fi in fields)
    //        {
    //            allPermissions.Add(new RoleClaimsViewModel { Value = fi.GetValue(null).ToString(), Type = "Permissions" });
    //        }
    //    }
    //    public static async Task AddPermissionClaim(this RoleManager<Role> roleManager, Role role, string permission)
    //    {
    //        var allClaims = await roleManager.GetClaimsAsync(role);
    //        if (!allClaims.Any(a => a.Type == "Permissions" && a.Value == permission))
    //        {
    //            await roleManager.AddClaimAsync(role, new Claim("Permissions", $"Permissions.{permission}"));
    //        }
    //    }
    //}
}
