using AuthServer.Infrastructure.Data.Identity;
using AuthService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
