using AuthService.Infrastructure.Data.Context;
using AuthService.Infrastructure.Data.Identity;
using AuthService.Models;
using AuthService.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    //public class ModuleRepository : IModuleRepository
    //{
    //    private readonly AppIdentityDbContext _dbContext;
    //    public ModuleRepository(AppIdentityDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }
    //    public void CreateModule(string nameModule, string description, string roleId)
    //    {
    //        _dbContext.Modules.Add(new Module { Name = nameModule, Description = description, RoleId = roleId });
    //        _dbContext.SaveChanges();
    //    }

    //    public void DeleteModule(string nameModule, string description, string roleId)
    //    {
    //        _dbContext.Modules.Remove(new Module { Name = nameModule, Description = description, RoleId = roleId});
    //        _dbContext.SaveChanges();
    //    }

    //    public async Task<IEnumerable<ModuleViewModel>> GetModules()
    //    {
    //        return await _dbContext.Modules.Select(s => new ModuleViewModel { Id = s.Id, Name = s.Name }).ToListAsync();
    //    }

    //    public async Task<ModuleViewModel> GetModuleById(int moduleId)
    //    {
    //        return await _dbContext.Modules.Where(s => s.Id == moduleId).Select(s => new ModuleViewModel { Id = s.Id, Name = s.Name }).FirstOrDefaultAsync();
    //    }
    //}
}
