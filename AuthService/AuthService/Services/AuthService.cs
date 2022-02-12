using AuthServer.Infrastructure.Data.Identity;
using AuthService;
using AuthService.Helpers;
using AuthService.Models;
using AuthService.Repositories.Interfaces;
using Grpc.Core;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WalletService.API.Handler.RSAHandler;
using System.Linq;
using System.Data.Entity;
using AuthService.Repositories.Interface;
using AuthService.Infrastructure.Data.Identity;
using AutoMapper;
using AuthService.Constants;
using Microsoft.AspNetCore.Authorization;

namespace AuthService.Services
{
    public class AuthProtoService : Auth.AuthBase
    {
        private readonly ILogger<AuthProtoService> _logger;

        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRSAHandler _rsaHandler;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public AuthProtoService(
            IMapper mapper,
            IClientRepository clientRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IRSAHandler rsaHandler,
            IConfiguration config,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            ILogger<AuthProtoService> logger)
        {
            _mapper = mapper;
            _clientRepository = clientRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _rsaHandler = rsaHandler;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = config;
            _logger = logger;
        }

        public override async Task<RegisterResponseModel> RegisterUser(RequestModel requestModel, ServerCallContext context)
        {
            try
            {
                RegisterResponseModel responseModel = new();

                var requestDecrypt = _rsaHandler.Decrypt(requestModel.Data);
                var requestInput = DeserializeHelper.DeserializeMethod<RegisterViewModel>(requestDecrypt);

                var clients = new List<Client>();
                foreach(var item in requestInput.ClientIds)
                {
                    clients.Add(await _clientRepository.GetClientById(Convert.ToInt32(item)));
                }

                var user = new User()
                {
                    UserName = requestInput.UserName,
                    Name = requestInput.FullName,
                    Email = requestInput.Email,
                    CreatedBy = "",
                    CreatedDate = DateTime.Now,
                    LastModifiedBy = "",
                    LastModifiedDate = DateTime.Now,
                    Clients = clients
                };

                if (requestInput.ClientIds.Count == 0)
                {
                    // lấy tất cả user tự do - không thuộc doanh nghiệp nào
                    var users = await _userRepository.GetUserFreedom();

                    // check mail đã tồn tại => false
                    if (users.Any(user => user.Email == requestInput.Email))
                    {
                        responseModel.Message = "Email đã tồn tại";
                        responseModel.Error = true;
                        return responseModel;
                    }
                } else
                {
                    // lấy tất cả user thuộc doanh nghiệp đó
                    var users = await _userRepository.GetUserByClients(requestInput.ClientIds);


                    // check mail đã tồn tại => false
                    if (users.Any(user => user.Email == requestInput.Email))
                    {
                        responseModel.Message = "Email đã tồn tại";
                        responseModel.Error = true;
                        return responseModel;
                    }
                }

                var result = await _userManager.CreateAsync(user, requestInput.Password);

                

                if (!result.Succeeded)
                {
                    //throw new Exception(result.Errors.First().Description);
                    responseModel.Message = result?.Errors?.FirstOrDefault()?.Description.ToString();
                    responseModel.Error = true;
                    return responseModel;
                }

                responseModel.Error = false;
                responseModel.Message = "Thành công";
                return responseModel;
            }
            catch (Exception ex)
            {
                RegisterResponseModel responseModel = new();
                responseModel.Message = ex.Message.ToString();
                responseModel.Error = true;
                return responseModel;
            }
        }

        public override async Task<LoginResponseModel> LoginUser(RequestModel requestModel, ServerCallContext context)
        {
            try
            {
                LoginResponseModel responseModel = new();

                var requestDecrypt = _rsaHandler.Decrypt(requestModel.Data);
                var requestInput = DeserializeHelper.DeserializeMethod<LoginViewModel>(requestDecrypt);

                var user = await _userRepository.GetUserByUserName(requestInput.UserName);

                if (user == null) 
                {
                    responseModel.Data = null;
                    responseModel.Message = "Không tồn tại " + requestInput.UserName;
                    responseModel.Error = true;
                    return responseModel;
                }

                if (user.Clients.Any(s => s.Id == Convert.ToInt32(requestInput.ClientId)))
                {
                    var result = await _signInManager
                        .CheckPasswordSignInAsync(user, requestInput.Password, false);

                    if (!result.Succeeded)
                    {
                        responseModel.Data = null;
                        responseModel.Message = "Không đúng password";
                        responseModel.Error = true;
                        return responseModel;
                    };
                } else
                {
                    responseModel.Data = null;
                    responseModel.Message = "User không có quyền ở hệ thống này";
                    responseModel.Error = true;
                    return responseModel;
                }



                UserLoginModel userLoginModel = new()
                {
                    Username = user.UserName,
                    Token = await TokenHelper.BuildToken(user, requestInput.ClientId, _userManager, _roleManager, _configuration),
                };
                responseModel.Data = userLoginModel;
                responseModel.Error = false;
                responseModel.Message = "Thành công";
                return responseModel;
            }
            catch (Exception ex)
            {
                LoginResponseModel responseModel = new();
                responseModel.Message = ex.Message.ToString();
                responseModel.Error = true;
                return responseModel;
            }
        }


        // cập nhật role cho user
        // UpdateRole
        public override async Task<UpdateRoleResponseModel> UpdateRoleUser(RequestModel requestModel, ServerCallContext context)
        {
            try
            {
                UpdateRoleResponseModel responseModel = new();

                var requestDecrypt = _rsaHandler.Decrypt(requestModel.Data);
                var requestInput = DeserializeHelper.DeserializeMethod<UserRolesViewModel>(requestDecrypt);

                await _roleRepository.UpdateRole(requestInput.Roles, requestInput.UserId);
                responseModel.Error = false;
                responseModel.Message = "Thành công";

                return responseModel;
            }
            catch (Exception ex)
            {
                UpdateRoleResponseModel responseModel = new();
                responseModel.Message = ex.Message.ToString();
                responseModel.Error = true;
                return responseModel;
            }
        }
        
        // GetTreeRoles
        [Authorize(Roles = "Link")]
        public override async Task<TreeRolesResponseModel> GetTreeRoles(RequestModel requestModel, ServerCallContext context)
        {
            try
            {
                TreeRolesResponseModel responseModel = new();

                var requestDecrypt = _rsaHandler.Decrypt(requestModel.Data);
                var requestInput = DeserializeHelper.DeserializeMethod<TreeRoleViewModel>(requestDecrypt);

                //var y = _mapper.Map<TreeRolesModel>(x);

                return await _roleRepository.GetTreeRoles(requestInput.ClientId);
            }
            catch (Exception ex)
            {
                TreeRolesResponseModel responseModel = new();
                responseModel.Message = ex.Message.ToString();
                responseModel.Error = true;
                return responseModel;
            }
        }
    }
}