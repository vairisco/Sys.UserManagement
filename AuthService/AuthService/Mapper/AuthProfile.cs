using AuthService.Models;
using AutoMapper;

namespace AuthService.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<TreeRoleResponseViewModel, TreeRolesModel>();
        }
    }
}
