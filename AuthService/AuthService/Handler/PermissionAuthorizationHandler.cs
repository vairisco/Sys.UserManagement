using AuthService.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;

namespace AuthService.Handler
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor = null;

        public PermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            var token = accessToken.ToString().Replace("Bearer", "").Trim();

            var handler = new JwtSecurityTokenHandler();
            if (!String.IsNullOrEmpty(token))
            {
                var jwtSecurityToken = handler.ReadToken(token) as JwtSecurityToken;

                var permissions = jwtSecurityToken.Claims.Where(x => x.Type == "Permissions" && x.Value == requirement.Permission).FirstOrDefault();

                if (permissions != null)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.FromResult(0);
        }
    }
}
