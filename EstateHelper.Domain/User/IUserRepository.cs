using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;


namespace EstateHelper.Domain.User
{
    public interface IUserRepository
    {
        Task<AppUser> SignUpUser (CreateUserDto request);
        Task<AppUser> SignUpGeneralAdmin (CreateUserDto request);
        Task<List<AppUser>> GetAllAsync();
        Task<AppUser> FindByIdAsync(string id);
        Task<AppUser> UpdateAsync(int id);

    }
}
