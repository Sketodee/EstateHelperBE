using EstateHelper.Application.Contract;
using EstateHelper.Application.Contract.Dtos.ConsultantGroups;
using EstateHelper.Application.Contract.Dtos.User;
using EstateHelper.Domain.HelperFunctions;
using EstateHelper.Domain.Models;
using System.Linq.Expressions;

namespace EstateHelper.Domain.ConsultantGroups
{
    public interface IConsultantGroupRepository
    {
        Task<ConsultantGroup> UpdateAsync(ConsultantGroup input);
        Task<ConsultantGroup> CreateAsync(ConsultantGroup input);
        Task<bool> DeleteAsync(ConsultantGroup input);
        Task<PagedResultDto<List<ConsultantGroup>>> GetAllAsync(PaginationParamaters pagination);
        Task<PagedResultDto<List<ConsultantGroup>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination);
        Task<ConsultantGroup> SingleOrDefaultAsync(Expression<Func<ConsultantGroup, bool>> predicate); 
    }
}
