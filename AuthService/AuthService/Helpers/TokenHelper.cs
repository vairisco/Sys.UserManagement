using AuthServer.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Helpers
{
    public static class TokenHelper
    {
        public static async Task<string> BuildToken(
            User user, 
            string clientId,
            UserManager<User> _userManager, 
            RoleManager<Role> _roleManager, 
            IConfiguration _configuration)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim("clientID", clientId));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));

                var role = await _roleManager.FindByNameAsync(userRole);

                if (role == null)
                {
                    continue;
                }

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (Claim roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:ValidIssuer"],
                Audience = _configuration["Host"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
