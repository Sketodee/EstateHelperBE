using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.Models;
using System.Linq.Expressions;


namespace EstateHelper.Domain.User
{
    public interface IUserRepository
    {
        Task<AppUser> SingleOrDefaultAsync(Expression<Func<AppUser, bool>> predicate);
        Task<List<AppUser>> GetAllAsync();
        Task<AppUser> FindByIdAsync(string id);
        Task<AppUser> UpdateAsync(int id);

    }
}
