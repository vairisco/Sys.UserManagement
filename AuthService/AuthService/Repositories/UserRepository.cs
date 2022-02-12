using AuthServer.Infrastructure.Data.Identity;
using AuthService.DTO;
using AuthService.Infrastructure.Data.Context;
using AuthService.Models;
using AuthService.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppIdentityDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly List<UserDTO> users = new List<UserDTO>();

        public UserRepository(AppIdentityDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<User>> GetUserByClients(List<string> clientIds)
        {
            var users = _dbContext.Users
                  .Include(user => user.Clients).Where(s => s.Clients.Any(client => clientIds.Contains(client.Id.ToString()))).ToList();
            return users;
        }

        public async Task<User> GetUserByUserName(string username)
        {
            var user = await _dbContext.Users.Include(s => s.Clients).Where(s => s.UserName == username).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<User>> GetUserFreedom()
        {
            var user = _dbContext.Users
                  .Include(user => user.Clients).Where(s => s.Clients.Count == 0).ToList();
            return user;
        }

        public async Task<bool> AddClientByUser(string userId, List<string> clientIds)
        {
            //var clientUser = _dbContext.
            return true;
        }
    }
}
