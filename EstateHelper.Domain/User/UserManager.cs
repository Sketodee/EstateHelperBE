using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.User
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUser> SignUpAdmin(CreateUserDto request)
        {
            var result = await _userRepository.SignUpAdmin(request);
            return result;
        }

        public async Task<AppUser> SignUpUser(CreateUserDto request)
        {
            var result = await _userRepository.SignUpUser(request);
            return result; 
        }
    }
}
