using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;


namespace EstateHelper.Domain.User
{
    public interface IUserRepository
    {
        Task<AppUser> SignUpUser (CreateUserDto request);
        Task<AppUser> SignUpAdmin (CreateUserDto request);
    }
}
