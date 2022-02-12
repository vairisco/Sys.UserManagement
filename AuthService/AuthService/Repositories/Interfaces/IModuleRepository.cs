using AuthService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Repositories.Interface
{
    public interface IModuleRepository
    {
        Task<IEnumerable<ModuleViewModel>> GetModules();
        void CreateModule(string nameModule, string description, string roleId);
        void DeleteModule(string nameModule, string description, string roleId);
        Task<ModuleViewModel> GetModuleById(int moduleId);
    }
}
