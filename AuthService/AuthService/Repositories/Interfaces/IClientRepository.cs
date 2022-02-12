using AuthServer.Infrastructure.Data.Identity;
using AuthService.DTO;
using AuthService.Infrastructure.Data.Identity;
using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories.Interfaces
{
    public interface IClientRepository
    {
        //UserDTO GetUser(LoginViewModel userMode);
        Task<Client> GetClientById(int clientId);
    }
}
