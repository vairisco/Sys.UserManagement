using AuthServer.Infrastructure.Data.Identity;
using AuthService.DTO;
using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        //UserDTO GetUser(LoginViewModel userMode);
        Task<List<User>> GetUserFreedom();
        Task<User> GetUserByUserName(string username);
        Task<List<User>> GetUserByClients(List<string> clientIds);
        Task<bool> AddClientByUser(string userId, List<string> clientIds);
    }
}
