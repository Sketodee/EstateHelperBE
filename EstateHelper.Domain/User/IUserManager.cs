using EstateHelper.Application.Contract.Dtos.Login;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.User
{
    public interface IUserManager
    {
        Task<AppUser> SignUpUser(CreateUserDto request);
        Task<AppUser> SignUpGeneralAdmin(CreateUserDto request);
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<GetLoggedInUserDto> GetLoggedInUser();  
        Task<string> GetRefreshToken();
        Task<string> Logout();
        Task<bool> AddUserToRole(string roleName, string userId);   
    }
}
