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
        Task<AppUser> SignUpAdmin(CreateUserDto request);
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<CreateUserDto> GetLoggedInUser();  
        Task<string> GetRefreshToken();
    }
}
