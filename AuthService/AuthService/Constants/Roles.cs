using AuthService.Models;
using System.Collections.Generic;

namespace AuthService.Constants
{
    public enum Roles
    {
        Create, // Tạo lập
        Review, // Kiểm soát
        Confirm // Duyệt lệnh
    }
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module, List<string>? actions)
        {
            List<string> permissionsForModule = new List<string>();
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    permissionsForModule.Add($"Permissions.{module}.{action}");
                }
            }
            else
            {
                permissionsForModule.Add($"Permissions.{module}");
            }
            return permissionsForModule;
        }
    }
}
