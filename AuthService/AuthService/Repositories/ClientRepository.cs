using AuthServer.Infrastructure.Data.Identity;
using AuthService.DTO;
using AuthService.Infrastructure.Data.Context;
using AuthService.Infrastructure.Data.Identity;
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
    public class ClientRepository : IClientRepository
    {
        private readonly AppIdentityDbContext _dbContext;

        public ClientRepository(AppIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Client> GetClientById(int clientId)
        {
            var client = await _dbContext.Clients.SingleOrDefaultAsync(s => s.Id == clientId);
            return client;
        }
    }
}
