using AutoMapper;
using EstateHelper.Application.Contract.Dtos.Login;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Application.Contract.Interface;
using EstateHelper.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public AuthService(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserDto> GetLoggedInUser()
        {
            var result = await _userManager.GetLoggedInUser();
            return result;
        }

        public async Task<string> GetRefreshToken()
        {
           var result = await _userManager.GetRefreshToken();
            return result;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
           var result = await _userManager.Login(request);
            return result;
        }

        public async Task<CreateUserDto> SignUpAdmin(CreateUserDto request)
        {
            var result = await _userManager.SignUpAdmin(request);
            return _mapper.Map<CreateUserDto>(result);
        }

        public async Task<CreateUserDto> SignUpUser(CreateUserDto request)
        {
            var result = await _userManager.SignUpUser(request);    
            return _mapper.Map<CreateUserDto>(result);
        }
    }
}
