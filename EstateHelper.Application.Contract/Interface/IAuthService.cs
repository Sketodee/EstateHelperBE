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
        Task<CreateUserDto> SignUpAdmin(CreateUserDto request); 
    }
}
