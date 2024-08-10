using EstateHelper.Application.Contract;
using EstateHelper.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstateHelper.Domain.Clients
{
    public interface IClientRepository
    {
        Task<Client> UpdateAsync(Client input);
        Task<Client> CreateAsync(Client input);
        Task<bool> DeleteAsync(Client input);
        Task<PagedResultDto<List<Client>>> GetAllAsync(PaginationParamaters pagination);
        Task<PagedResultDto<List<Client>>> GetAllByFilter(string? Id, string? queryParam, PaginationParamaters pagination);
        Task<Client> SingleOrDefaultAsync(Expression<Func<Client, bool>> predicate);
    }
}
