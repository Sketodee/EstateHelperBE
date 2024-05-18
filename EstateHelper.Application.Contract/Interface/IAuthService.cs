using EstateHelper.Application.Contract.Dtos.Login;
using EstateHelper.Application.Contract.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Contract.Interface
{
    public interface IAuthService
    {
        Task<CreateUserDto> SignUpUser(CreateUserDto request); 
        Task<CreateUserDto> SignUpGeneralAdmin(CreateUserDto request); 
        Task<LoginResponseDto> Login(LoginRequestDto request);
        Task<CreateUserDto> GetLoggedInUser();
        Task<string> GetRefreshToken();
    }
}
